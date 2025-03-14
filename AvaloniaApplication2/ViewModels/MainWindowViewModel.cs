using System.Collections.ObjectModel;
using System.Reactive;
using AvaloniaApplication2.Models.DTOs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<ContactData> Contacts { get; } = new();
    public ReactiveCommand<Unit, Unit> ShowContactsCommand { get; }
    
    [Reactive]
    public string SearchQuery { get; set; }
}