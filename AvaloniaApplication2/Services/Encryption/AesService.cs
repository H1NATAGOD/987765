using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AvaloniaApplication2.Models.DTOs;
using Microsoft.Extensions.Configuration;

public class AesService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;
    
    public AesService(IConfiguration config)
    {
        var passphrase = config["EncryptionKey"];
        using var deriveBytes = new Rfc2898DeriveBytes(passphrase, 16);
        _key = deriveBytes.GetBytes(32);
        _iv = deriveBytes.GetBytes(16);
    }

    public byte[] Encrypt(ContactData data)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        // Сериализация данных в JSON и конвертация в байты
        var json = JsonSerializer.Serialize(data);
        var dataBytes = Encoding.UTF8.GetBytes(json);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        {
            cs.Write(dataBytes, 0, dataBytes.Length);
            cs.FlushFinalBlock();
        }

        return ms.ToArray();
    }

    public ContactData Decrypt(byte[] encrypted)
    {
        using var aes = Aes.Create();
        aes.Key = _key;
        aes.IV = _iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(encrypted);
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cs, Encoding.UTF8);
        
        // Десериализация из JSON
        var json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<ContactData>(json);
    }
}