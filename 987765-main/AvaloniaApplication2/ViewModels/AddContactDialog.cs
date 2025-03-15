using ReactiveUI;
using System.Reactive;
using System;
using AvaloniaApplication2.Models.DTOs;
using AvaloniaApplication2.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication2.ViewModels
{
    public class AddContactDialogViewModel : ViewModelBase
    {
        // Команды
        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }
        
        // События для коммуникации с окном
        public event Action<ContactData>? OnSubmit;
        public event Action? OnCancel;

        public AddContactDialogViewModel()
        {
            SaveCommand = ReactiveCommand.Create(() => 
            {
                var contact = new ContactData 
                {
                    // Заполнение данных из полей
                };
                
                OnSubmit?.Invoke(contact);
            });
            
            CancelCommand = ReactiveCommand.Create(() => OnCancel?.Invoke());
        }
    }
}