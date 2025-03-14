using System.ComponentModel.DataAnnotations.Schema;
using AvaloniaApplication2.Models.DTOs;

namespace AvaloniaApplication2.Models;

public class PhysicalContact
{
    public int Id { get; set; }
    
 
    public byte[] EncryptedData { get; set; }
    
    public string SearchHash { get; set; } // SHA256 хеш для поиска
    
    public int? LegalEntityId { get; set; }
    public LegalContact LegalEntity { get; set; }
    
    [NotMapped]
    public ContactData Data { get; set; } // Дешифрованные данные
}