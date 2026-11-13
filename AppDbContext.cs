using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Asset> Assets {get; set; }
    
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
        .UseSqlServer(
            @"Server=localhost\SQLEXPRESS;Database=AssetTrackerDB;Trusted_Connection=True;TrustServerCertificate=True;")
        .LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Computer>(); 
        modelBuilder.Entity<Mobile>(); 
    }
}