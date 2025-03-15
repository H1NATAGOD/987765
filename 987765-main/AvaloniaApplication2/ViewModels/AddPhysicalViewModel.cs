// ViewModels/Dialogs/AddPhysicalViewModel.cs
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using AvaloniaApplication2.Views.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication2.ViewModels.Dialogs
{
    public class AddPhysicalViewModel : ViewModelBase
    {
        [Required(ErrorMessage = "Поле обязательно")]
        [RegularExpression(@"^\+?[\d\s-]{7,}$", ErrorMessage = "Неверный формат")]
        [Reactive]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [Reactive]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Reactive]
        public string FirstName { get; set; } = string.Empty;

        public ReactiveCommand<Unit, Unit> SaveCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public AddPhysicalViewModel(AddPhysicalDialog dialog)
        {
            _dialog = dialog; // Сохраняем ссылку на диалог
        
            var canSave = this.WhenAnyValue(
                x => x.LastName,
                x => x.Phone,
                (name, phone) => 
                    !string.IsNullOrWhiteSpace(name) && 
                    !string.IsNullOrWhiteSpace(phone)
            );
        
            SaveCommand = ReactiveCommand.Create(Save, canSave);
            CancelCommand = ReactiveCommand.Create(() => _dialog.Close(false));
        }

        private void Save()
        {
            try
            {
                // Валидация
                var context = new ValidationContext(this);
                Validator.ValidateObject(this, context, true);

                // Создаем результат
                var result = new PhysicalContactData 
                {
                    LastName = LastName,
                    FirstName = FirstName,
                    Phone = Phone
                };

                // Закрываем диалог с результатом
                _dialog.CloseWithResult(result);
            }
            catch (ValidationException ex)
            {
                // Показываем ошибку в UI
                ErrorMessage = ex.Message;
            }
        }

        [Reactive]
        public string ErrorMessage { get; set; } // Новое свойство для ошибок
}