using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Configuration;

public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("driver");

        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.Name, "name").IsUnique();

        builder.Property(e => e.Id).HasColumnType("int(11)").HasColumnName("id");

        builder.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");

        builder.Property(e => e.Age).HasMaxLength(45).HasColumnName("age");
    }
}
