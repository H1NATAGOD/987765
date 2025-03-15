using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.Models.DTOs;
using AvaloniaApplication2.Services;
using AvaloniaApplication2.Services.Storage;
using AvaloniaApplication2.Views;
using AvaloniaApplication2.Views.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication2.ViewModels;

public class EmployeeViewModel : UserMainViewModel
{
    public ReactiveCommand<Unit, Unit> AddPhysicalCommand { get; }
    public ReactiveCommand<Unit, Unit> AddJuridicalCommand { get; }

    [Reactive] public PhysicalContact? SelectedPhysical { get; set; }
    [Reactive] public LegalContact? SelectedJuridical { get; set; }

    public EmployeeViewModel(AppDbContext db, AesService aes,PersonalBookService requiredService) : base(db, aes, requiredService)
    {
        AddPhysicalCommand = ReactiveCommand.CreateFromTask(AddPhysicalContact);
        AddJuridicalCommand = ReactiveCommand.CreateFromTask(AddJuridicalContact);
    }

   
    services.AddTransient<AddPhysicalDialog>();
    services.AddTransient<AddJuridicalDialog>();


// ViewModels/EmployeeMainViewModel.cs
    // EmployeeMainViewModel.cs
    private async Task AddPhysicalContact()
    {
        var dialog = new AddPhysicalDialog();
        await dialog.ShowDialog(this.View);
    
        if (dialog.Result != null)
        {
            var encrypted = _aes.Encrypt(dialog.Result);
            // ... сохранение в БД ...
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