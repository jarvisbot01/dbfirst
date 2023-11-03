using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura.Configuration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("team");

        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.Name, "name").IsUnique();

        builder.Property(e => e.Id).HasColumnType("int(11)").HasColumnName("id");

        builder.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");

        builder
            .HasMany(d => d.IdDrivers)
            .WithMany(p => p.IdTeams)
            .UsingEntity<Dictionary<string, object>>(
                "TeamDriver",
                r =>
                    r.HasOne<Driver>()
                        .WithMany()
                        .HasForeignKey("IdDriver")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("teamDriver_ibfk_1"),
                l =>
                    l.HasOne<Team>()
                        .WithMany()
                        .HasForeignKey("IdTeam")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("teamDriver_ibfk_2"),
                j =>
                {
                    j.HasKey("IdTeam", "IdDriver")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    j.ToTable("teamDriver");
                    j.HasIndex(new[] { "IdDriver" }, "idDriver");
                    j.IndexerProperty<int>("IdTeam")
                        .HasColumnType("int(11)")
                        .HasColumnName("idTeam");
                    j.IndexerProperty<int>("IdDriver")
                        .HasColumnType("int(11)")
                        .HasColumnName("idDriver");
                }
            );
    }
}
