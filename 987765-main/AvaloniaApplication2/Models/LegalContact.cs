using System.ComponentModel.DataAnnotations.Schema;



namespace AvaloniaApplication2.Models;

public class LegalContact
{
    public int Id { get; set; }
    

    public byte[] EncryptedData { get; set; }
    
    [NotMapped]
    public CompanyData Data { get; set; }
}

public class CompanyData
{
    public string Name { get; set; }
    public string TaxId { get; set; }
    public string RegistrationNumber { get; set; }
}