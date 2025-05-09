using Microsoft.EntityFrameworkCore;
using Graduation.Models;

namespace Graduation.Infrastructure;



public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<GraduationDetail> GraduationDetails { get; set; }
}