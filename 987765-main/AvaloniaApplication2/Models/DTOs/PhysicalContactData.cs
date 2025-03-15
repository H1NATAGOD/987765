using System;
using System.ComponentModel.DataAnnotations;

namespace AvaloniaApplication2.Models.DTOs
{
    public class PhysicalContactData
    {
        [Required(ErrorMessage = "Фамилия обязательна")]
        [StringLength(50, ErrorMessage = "Максимум 50 символов")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(50, ErrorMessage = "Максимум 50 символов")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "Максимум 50 символов")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Телефон обязателен")]
        [RegularExpression(@"^\+7 \d{3} \d{3}-\d{2}-\d{2}$", 
            ErrorMessage = "Формат: +7 XXX XXX-XX-XX")]
        public string Phone { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Максимум 200 символов")]
        public string? Address { get; set; }

        public byte[]? Photo { get; set; }
        
        public Guid? LegalEntityId { get; set; }
    }
}