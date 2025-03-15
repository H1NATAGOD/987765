using System.Reactive;
using AvaloniaApplication2.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using AddJuridicalDialog = AvaloniaApplication2.Views.Dialogs.AddJuridicalDialog;
using Views_AddJuridicalDialog = AvaloniaApplication2.Views.AddJuridicalDialog;

namespace AvaloniaApplication2.ViewModels;

public class AddJuridicalViewModel : ViewModelBase
{

    [Reactive] public string LastName { get; set; } = string.Empty;
    [Reactive] public string FirstName { get; set; } = string.Empty;
    [Reactive] public string Phone { get; set; } = string.Empty;

    public ReactiveCommand<Unit, Unit> SaveCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    public AddJuridicalViewModel(Views_AddJuridicalDialog dialog)
    {
        _dialog = dialog;

        SaveCommand = ReactiveCommand.Create(() =>
        {
            _dialog.Result = new PhysicalContactData
            {
                LastName = LastName,
                FirstName = FirstName,
                Phone = Phone
            };
            _dialog.Close(true);
        });

        CancelCommand = ReactiveCommand.Create(() => _dialog.Close(false));
    }
}