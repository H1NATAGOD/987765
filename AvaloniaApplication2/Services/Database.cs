using AvaloniaApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication2.Services;

public class Database
{
 
    public class AppDbContext : DbContext
    {
        public DbSet<PhysicalContact> PhysicalContacts { get; set; }
        public DbSet<LegalContact> LegalContacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Host=10.30.0.137;Database=gr624_addan;Username=gr624_addan;Password=RRUatiM3f5y#2");
    }
}