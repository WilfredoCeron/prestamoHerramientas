using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prestamoHerramientas.Models;

public partial class PrestamosContext : DbContext
{
    public PrestamosContext()
    {
    }

    public PrestamosContext(DbContextOptions<PrestamosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipo> Equipos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<TipoHerramienta> TipoHerramientas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer("Data Source=WILFREDOCERON\\SQLEXPRESS01;Initial Catalog=PRESTAMOS;Integrated Security=True; TrustServerCertificate=True;"
);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipo>(entity =>
        {
            entity.HasKey(e => e.IdEquipo).HasName("PK__equipos__981ACF53810023F3");

            entity.ToTable("equipos");

            entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdMarca).HasColumnName("idMarca");
            entity.Property(e => e.NombreEquipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreEquipo");
            entity.Property(e => e.NumeroSerie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numeroSerie");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Equipos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__equipos__idMarca__2C3393D0");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__marcas__70331812A5421F81");

            entity.ToTable("marcas");

            entity.Property(e => e.IdMarca).HasColumnName("idMarca");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Exactitud)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("exactitud");
            entity.Property(e => e.IdTipoHerramienta).HasColumnName("idTipoHerramienta");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreMarca");

            entity.HasOne(d => d.IdTipoHerramientaNavigation).WithMany(p => p.Marcas)
                .HasForeignKey(d => d.IdTipoHerramienta)
                .HasConstraintName("FK__marcas__idTipoHe__267ABA7A");
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.IdModelo).HasName("PK__modelos__13A52CD19C8305FD");

            entity.ToTable("modelos");

            entity.Property(e => e.IdModelo).HasColumnName("idModelo");
            entity.Property(e => e.IdMarca).HasColumnName("idMarca");
            entity.Property(e => e.Serie)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serie");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__modelos__idMarca__29572725");
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.HasKey(e => e.IdPrestamo).HasName("PK__prestamo__A4876C13E4C96C69");

            entity.ToTable("prestamos");

            entity.Property(e => e.IdPrestamo).HasColumnName("idPrestamo");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaFin)
                .HasColumnType("date")
                .HasColumnName("fechaFin");
            entity.Property(e => e.FechaIncio)
                .HasColumnType("date")
                .HasColumnName("fechaIncio");
            entity.Property(e => e.IdMarca).HasColumnName("idMarca");
            entity.Property(e => e.IdModelo).HasColumnName("idModelo");
            entity.Property(e => e.PreatadoA)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("preatadoA");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__prestamos__idMar__2F10007B");

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Prestamos)
                .HasForeignKey(d => d.IdModelo)
                .HasConstraintName("FK__prestamos__idMod__300424B4");
        });

        modelBuilder.Entity<TipoHerramienta>(entity =>
        {
            entity.HasKey(e => e.IdTipoHerramienta).HasName("PK__tipo_her__246436BEECE1F005");

            entity.ToTable("tipo_herramientas");

            entity.Property(e => e.IdTipoHerramienta).HasColumnName("idTipoHerramienta");
            entity.Property(e => e.TipoHerramienta1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoHerramienta");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
