using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjetV3R_Employe.Data.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Email>(entity =>
        {
            entity.HasKey(e => e.IdEmail).HasName("PRIMARY");

            entity.ToTable("email");

            entity.Property(e => e.IdEmail)
                .HasColumnType("int(11)")
                .HasColumnName("idEmail");
            entity.Property(e => e.BodyEmail)
                .HasColumnType("text")
                .HasColumnName("bodyEmail");
            entity.Property(e => e.CcEmail)
                .HasMaxLength(64)
                .HasColumnName("ccEmail");
            entity.Property(e => e.FromEmail)
                .HasMaxLength(64)
                .HasColumnName("fromEmail");
            entity.Property(e => e.ObjetEmail)
                .HasMaxLength(64)
                .HasColumnName("objetEmail");
            entity.Property(e => e.TitreEmail)
                .HasMaxLength(32)
                .HasColumnName("titreEmail");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRole)
                .HasColumnType("int(11)")
                .HasColumnName("idRole");
            entity.Property(e => e.NomRole)
                .HasMaxLength(32)
                .HasColumnName("nomRole");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.Role, "role");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(64)
                .HasColumnName("email");
            entity.Property(e => e.Role)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)")
                .HasColumnName("role");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
