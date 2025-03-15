using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication2.Models.DTOs;
using AvaloniaApplication2.ViewModels;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication2.Views
{
   

public ReactiveCommand<Unit, Unit> ForceUpdateCommand { get; }

private void InitializeCommands()
{
    ForceUpdateCommand = ReactiveCommand.CreateFromTask(ForceSystemUpdate);
}

private async Task ForceSystemUpdate()
{
    using var transaction = await _db.Database.BeginTransactionAsync();
    try
    {
        // 1. Применение миграций
        await _db.Database.MigrateAsync();

        // 2. Очистка кешей
        await ClearCaches();

        // 3. Пересчет хешей
        await RecalculateSearchHashes();

        // 4. Валидация данных
        var validationErrors = await ValidateData();
        if (validationErrors.Any())
        {
            throw new InvalidOperationException(
                $"Найдены ошибки данных: {string.Join(", ", validationErrors)}");
        }

        await transaction.CommitAsync();
        
        // 5. Обновление UI
        LoadContacts();
        LoadJuridicalContacts();
        
        // Логирование успеха
        await LogAction("Система успешно обновлена", LogLevel.Info);
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        await LogAction($"Ошибка обновления: {ex.Message}", LogLevel.Error);
        throw;
    }
}

private async Task ClearCaches()
{
    // Очистка кеша изображений
    var cacheDir = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Cache");
    
    if (Directory.Exists(cacheDir))
    {
        Directory.Delete(cacheDir, true);
        Directory.CreateDirectory(cacheDir);
    }

    // Сброс внутренних кешей
    _db.ChangeTracker.Clear();
    await Task.CompletedTask;
}

private async Task RecalculateSearchHashes()
{
    var contacts = await _db.PhysicalContacts.ToListAsync();
    foreach (var contact in contacts)
    {
        var data = _aes.Decrypt(contact.EncryptedData);
        contact.SearchHash = ComputeSearchHash(data.LastName);
    }
    await _db.SaveChangesAsync();
}

private async Task<List<string>> ValidateData()
{
    var errors = new List<string>();
    
    // Проверка юридических лиц
    var invalidLegal = await _db.LegalContacts
        .Where(l => string.IsNullOrEmpty(l.EncryptedData))
        .ToListAsync();

    if (invalidLegal.Any())
        errors.Add($"Найдено {invalidLegal.Count} юр. лиц с пустыми данными");

    // Проверка физических лиц
    var invalidPhysical = await _db.PhysicalContacts
        .Where(p => string.IsNullOrEmpty(p.SearchHash))
        .ToListAsync();

    if (invalidPhysical.Any())
        errors.Add($"Найдено {invalidPhysical.Count} физ. лиц с некорректными хешами");

    return errors;
}

private async Task LogAction(string message, LogLevel level)
{
    await _db.Logs.AddAsync(new SystemLog
    {
        Timestamp = DateTime.UtcNow,
        Message = message,
        Level = level.ToString()
    });
    await _db.SaveChangesAsync();
}
}