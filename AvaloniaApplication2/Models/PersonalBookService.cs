using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using AvaloniaApplication2.Models.DTOs;

namespace AvaloniaApplication2.Services.Storage;

using System.Text.Json;
using System.IO;
using System;

public class PersonalBookService
{
    public void SaveToJson(List<ContactData> contacts, string userId)
    {
        // Получаем путь к папке пользователя
        var userFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "AddressBook",
            userId
        );

        // Создаем директорию, если не существует
        Directory.CreateDirectory(userFolder);

        // Формируем полный путь к файлу
        var filePath = Path.Combine(userFolder, "contacts.json");
        
        // Сериализуем с отступами для читаемости
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(filePath, JsonSerializer.Serialize(contacts, options));
    }

    
}