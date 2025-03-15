using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; // Добавьте это пространство имён
using System.IO;
using AvaloniaApplication2.Models;
using AvaloniaApplication2.Models.DTOs;
using AvaloniaApplication2.Services; 

public class AppDbContext : DbContext
{
    public DbSet<PhysicalContact> PhysicalContacts { get; set; }
    public DbSet<LegalContact> LegalContacts { get; set; }
    public DbSet<Auth.User> Users { get; set; }
    public DbSet<SystemLog> Logs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // 1. Инициализация конфигурации
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // 2. Получение строки подключения
        options.UseNpgsql(configuration.GetConnectionString("Default"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhysicalContact>(e =>
        {
            e.Property(p => p.EncryptedData)
                .HasColumnType("bytea")
                .IsRequired();
            
            e.HasIndex(p => p.SearchHash)
                .HasMethod("hash");
        });

        modelBuilder.Entity<LegalContact>(e =>
        {
            e.Property(l => l.EncryptedData)
                .HasColumnType("bytea")
                .IsRequired();
        });
    }
}