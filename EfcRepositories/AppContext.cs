using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepositories;

public class AppContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Comment> Comments => Set<Comment>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=/Users/apogultekin/Library/CloudStorage/OneDrive-ViaUC/Skole_/3 semester/DNP1/DNP1_Assignment/EfcRepositories/app.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}