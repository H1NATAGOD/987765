using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.Models.DTOs;
using AvaloniaApplication2.Utils;

namespace AvaloniaApplication2.Repositories;

public class ContactRepository
{
    private readonly AppDbContext _context;
    private readonly AesService _encryptor;

    public ContactRepository(AppDbContext context, AesService encryptor)
    {
        _context = context;
        _encryptor = encryptor;
    }

    public async Task AddPhysicalContact(ContactData data)
    {
        var contact = new PhysicalContact
        {
            EncryptedData = _encryptor.Encrypt(data),
            SearchHash = ComputeSearchHash(data.LastName),
            LegalEntityId = data.LegalEntityId
        };

        await _context.PhysicalContacts.AddAsync(contact);
        await _context.SaveChangesAsync();
    }

    private string ComputeSearchHash(string input)
    {
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}