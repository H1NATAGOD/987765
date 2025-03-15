namespace AvaloniaApplication2.Models.DTOs;

public class ContactData
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public byte[] Photo { get; set; }
    public int Id {get; set;}
    public string CompanyName { get; set; }
    
    public bool IsLegalEntity { get; set; }
    public string LegalEntityName { get; set; }
    public int LegalEntityId { get; set; }
}