using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Data;

public partial class DbfirstContext : DbContext
{
    public DbfirstContext() { }

    public DbfirstContext(DbContextOptions<DbfirstContext> options)
        : base(options) { }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");
    }
}
