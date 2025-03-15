using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.Services.Storage;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AvaloniaApplication2.ViewModels;

public class UserMainViewModel : ReactiveObject
{
    protected readonly AppDbContext _db;
    protected readonly AesService _aes;
    
    public ObservableCollection<PhysicalContact> PhysicalContacts { get; } = new();
    public ObservableCollection<LegalContact> LegalContacts { get; } = new();

    public UserMainViewModel(AppDbContext db, AesService aes, PersonalBookService requiredService)
    {
        _db = db;
        _aes = aes;
        LoadContacts();
        LoadJuridicalContacts();
    }

    protected async void LoadContacts()
    {
        var contacts = await _db.PhysicalContacts.ToListAsync();
        PhysicalContacts.Clear();
        foreach (var c in contacts)
        {
            PhysicalContacts.Add(c);
        }
    }

    protected async void LoadJuridicalContacts()
    {
        var contacts = await _db.LegalContacts.ToListAsync();
        LegalContacts.Clear();
        foreach (var c in contacts)
        {
            LegalContacts.Add(c);
        }
    }
}