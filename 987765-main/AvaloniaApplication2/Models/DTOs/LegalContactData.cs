using System.ComponentModel.DataAnnotations;

namespace AvaloniaApplication2.Models.DTOs
{
    public class LegalContactData
    {
        [Required(ErrorMessage = "Название организации обязательно")]
        [StringLength(100, ErrorMessage = "Максимум 100 символов")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ИНН обязателен")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ИНН должен содержать 10 цифр")]
        public string TaxId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Юридический адрес обязателен")]
        [StringLength(200, ErrorMessage = "Максимум 200 символов")]
        public string LegalAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Контактный телефон обязателен")]
        [RegularExpression(@"^\+7 \d{3} \d{3}-\d{2}-\d{2}$", 
            ErrorMessage = "Формат: +7 XXX XXX-XX-XX")]
        public string ContactPhone { get; set; } = string.Empty;
    }
    
    
}