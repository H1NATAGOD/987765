using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using AvaloniaApplication2.Models.DTOs;

using AvaloniaApplication2.Services;
using AvaloniaApplication2.ViewModels;
using ReactiveUI;
using Microsoft.EntityFrameworkCore;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Avalonia.Input;
using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Serilog;
using AvaloniaApplication2.Repositories;
using AvaloniaApplication2.Views.Dialogs;

namespace AvaloniaApplication2.Views;

public partial class EmployeeMainViewModel : UserMainViewModel
{
    public ReactiveCommand<Unit,Unit> AddPhysicalCommand { get; }

    public EmployeeMainViewModel(AppDbContext db, AesService aes)
    {
        AddPhysicalCommand = ReactiveCommand.CreateFromTask(AddPhysicalContact);
    }

    private async Task AddPhysicalContact()
    {
        var dialog = new AddContactDialog();
        if (await dialog.ShowAsync<ContactData?>(this))
        {
            var encrypted = _aes.Encrypt(dialog.Result);
            _db.SaveChangesAsync();
            await _db.SaveChangesAsync();
        }
    }
}