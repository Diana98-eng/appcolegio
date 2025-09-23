using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace appcolegio.Models;

public partial class ColegioContext : DbContext
{
    public ColegioContext()
    {
    }

    public ColegioContext(DbContextOptions<ColegioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Materium> Materia { get; set; }

    public virtual DbSet<Notum> Nota { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__estudian__3213E83F3AAF3275");

            entity.ToTable("estudiantes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Materium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__materia__3213E83F1BC49B50");

            entity.ToTable("materia");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NomMateria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom_materia");
        });

        modelBuilder.Entity<Notum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__nota__3213E83FE1B239F5");

            entity.ToTable("nota");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdMateria).HasColumnName("id_materia");
            entity.Property(e => e.Nota)
                .HasColumnType("decimal(3, 1)")
                .HasColumnName("nota");

            entity.HasOne(d => d.oEstudiante).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("fk_estudiantes");

            entity.HasOne(d => d.oMateria).WithMany(p => p.Nota)
                .HasForeignKey(d => d.IdMateria)
                .HasConstraintName("fk_materias");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
