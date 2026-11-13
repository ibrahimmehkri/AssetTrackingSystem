using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class AppDbContext : DbContext
{
    public DbSet<Asset> Assets {get; set; }
    
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
        .UseSqlServer(
            @"Server=localhost\SQLEXPRESS;Database=AssetTrackerDB;Trusted_Connection=True;TrustServerCertificate=True;")
        .LogTo(Console.WriteLine, LogLevel.Warning);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Computer>(); 
        modelBuilder.Entity<Mobile>(); 
    }
}