using System.Reactive;
using System.Threading.Tasks;
using AvaloniaApplication2.Models.DTOs;
using AvaloniaApplication2.Services.Storage;
using AvaloniaApplication2.ViewModels;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using AvaloniaApplication2.;


namespace AvaloniaApplication2.ViewModels;

public partial class AdminDashboardViewModel : UserMainViewModel
{
    public ReactiveCommand<Unit,Unit> AddPhysicalCommand { get; }

    public AdminDashboardViewModel(AppDbContext db, AesService aes, PersonalBookService personalBookService) : base(db, aes, personalBookService)
    {
        AddPhysicalCommand = ReactiveCommand.CreateFromTask(AddPhysicalContact);
    }

    private async Task AdminDashboardContact()
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