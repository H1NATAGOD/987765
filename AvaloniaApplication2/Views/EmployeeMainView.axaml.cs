// EmployeeMainViewModel.cs

using System;
using System.Reactive;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.Services;
using AvaloniaApplication2.Services.Storage;
using AvaloniaApplication2.Views.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication2.ViewModels;

public class EmployeeMainViewModel : UserMainViewModel
{
    public ReactiveCommand<Unit, Unit> AddPhysicalCommand { get; }
    public ReactiveCommand<Unit, Unit> AddJuridicalCommand { get; }

    [Reactive] public PhysicalContact? SelectedPhysical { get; set; }
    [Reactive] public LegalContact? SelectedJuridical { get; set; }

    public EmployeeMainViewModel(AppDbContext db, AesService aes,PersonalBookService requiredService) : base(db, aes, requiredService)
    {
        AddPhysicalCommand = ReactiveCommand.CreateFromTask(AddPhysicalContact);
        AddJuridicalCommand = ReactiveCommand.CreateFromTask(AddJuridicalContact);
    }

    private async Task AddPhysicalContact()
    {
        var dialog = new AddPhysicalDialog();
        var result = await dialog.ShowDialog<PhysicalContactData?>(this);
        
        if (result != null)
        {
            var encrypted = _aes.Encrypt(result);
            _db.PhysicalContacts.Add(new PhysicalContact 
            {
                EncryptedData = encrypted,
                SearchHash = ComputeSearchHash(result.LastName)
            });
            await _db.SaveChangesAsync();
            LoadContacts(); // Обновляем список
        }
    }

    private async Task AddJuridicalContact()
    {
        var dialog = new AddJuridicalDialog();
        var result = await dialog.ShowDialog<LegalContactData?>(this);
        
        if (result != null)
        {
            var encrypted = _aes.Encrypt(result);
            _db.LegalContacts.Add(new LegalContact 
            {
                EncryptedData = encrypted
            });
            await _db.SaveChangesAsync();
            LoadJuridicalContacts(); // Метод для загрузки юр. лиц
        }
    }

    private string ComputeSearchHash(string input)
    {
        using var sha = SHA256.Create();
        return Convert.ToBase64String(
            sha.ComputeHash(Encoding.UTF8.GetBytes(input))
        );
    }
}