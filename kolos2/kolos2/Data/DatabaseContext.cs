
using kolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace kolos2.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Items> items { get; set; }
    public DbSet<Backpacks> backpacks { get; set; }
    public DbSet<Characters> characters { get; set; }
    public DbSet<Character_titles> character_titles { get; set; }
    public DbSet<Titles> titles { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Items>().HasData(new List<Items>()
        {
            new() { Id = 1, Name = "Item1", Weight = 10 },
            new() { Id = 2, Name = "Item2", Weight = 20 },
            new() { Id = 3, Name = "Item3", Weight = 30 }
        });

        modelBuilder.Entity<Backpacks>().HasKey(b => new { b.CharacterId, b.ItemId });
        modelBuilder.Entity<Backpacks>().HasData(new List<Backpacks>()
        {
            new() { CharacterId = 1, ItemId = 1, Amount = 1 },
            new() { CharacterId = 2, ItemId = 2, Amount = 1 },
            new() { CharacterId = 3, ItemId = 3, Amount = 1 }
        });

        modelBuilder.Entity<Characters>().HasData(new List<Characters>()
        {
            new() { Id = 1, FirstName = "Kamil", LastName = "Witkowski", CurrentWei = 1, MaxWeight = 100 },
            new() { Id = 2, FirstName = "Name2", LastName = "Lastname2", CurrentWei = 2, MaxWeight = 200 },
            new() { Id = 3, FirstName = "Name3", LastName = "Lastname3", CurrentWei = 3, MaxWeight = 300 }
        });

        modelBuilder.Entity<Character_titles>().HasKey(ct => new { ct.CharacterId, ct.TitleId });
        modelBuilder.Entity<Character_titles>().HasData(new List<Character_titles>()
        {
            new() { CharacterId = 1, TitleId = 1, AcquireAt = new DateTime() },
            new() { CharacterId = 2, TitleId = 2, AcquireAt = new DateTime() },
            new() { CharacterId = 3, TitleId = 3, AcquireAt = new DateTime() }
        });

        modelBuilder.Entity<Titles>().HasData(new List<Titles>()
        {
            new() { Id = 1, Name = "Title1" },
            new() { Id = 2, Name = "Title2" },
            new() { Id = 3, Name = "Title3" }
        });
    }
}
