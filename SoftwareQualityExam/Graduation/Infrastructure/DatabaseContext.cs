using Graduation.Models;
using Microsoft.EntityFrameworkCore;

namespace Graduation.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<GraduationDetail> GraduationDetails { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<User>()
            .Property(u => u.Rank)
            .HasConversion<int>();
    }
}