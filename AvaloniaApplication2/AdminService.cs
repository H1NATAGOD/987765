using System.Collections.Generic;
using AvaloniaApplication2.Models.DTOs;
using Splat;
using AvaloniaApplication2.Services;
using AvaloniaApplication2.Services.Encryption;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication2;

public class AdminService
{
    public void ForceSystemUpdate(AppDbContext _db)
    {
        // Обновление схемы БД
        using var transaction = _db.Database.BeginTransaction();
        try
        {
            _db.Database.Migrate();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public List<SystemLog> GetErrorLogs(AppDbContext _db)
    {
        return _db.Logs
            .Where(l => l.LogLevel == LogLevel.Error)
            .OrderByDescending(l => l.Timestamp)
            .ToList();
    }
}