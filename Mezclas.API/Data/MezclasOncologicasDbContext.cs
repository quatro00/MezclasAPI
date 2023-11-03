using System;
using System.Collections.Generic;
using Mezclas.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Data;

public partial class MezclasOncologicasDbContext : DbContext
{
    public MezclasOncologicasDbContext()
    {
    }

    public MezclasOncologicasDbContext(DbContextOptions<MezclasOncologicasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatalogoArticulo> CatalogoArticulos { get; set; }

    public virtual DbSet<CuentaRol> CuentaRols { get; set; }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-540B3V8;Initial Catalog=MezclasOncologicasDb;Persist Security Info=True;User ID=sa;Password=sql2;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatalogoArticulo>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CantidadPresentacion).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.CuadroBasicoSai)
                .HasMaxLength(50)
                .HasColumnName("CuadroBasicoSAI");
            entity.Property(e => e.DescripcionArticuloCorta).HasColumnName("descripcionArticuloCorta");
            entity.Property(e => e.Dif)
                .HasMaxLength(10)
                .HasColumnName("dif");
            entity.Property(e => e.Esp)
                .HasMaxLength(10)
                .HasColumnName("esp");
            entity.Property(e => e.Gen)
                .HasMaxLength(10)
                .HasColumnName("gen");
            entity.Property(e => e.Gpo)
                .HasMaxLength(10)
                .HasColumnName("gpo");
            entity.Property(e => e.NivelDeAtencion)
                .HasMaxLength(500)
                .HasColumnName("nivelDeAtencion");
            entity.Property(e => e.PartidaPresupuestal).HasMaxLength(50);
            entity.Property(e => e.PrecioArticulo).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ProgramaEspecial)
                .HasMaxLength(500)
                .HasColumnName("programaEspecial");
            entity.Property(e => e.TipoPresentacion).HasMaxLength(50);
            entity.Property(e => e.UnidadPresentacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Var)
                .HasMaxLength(10)
                .HasColumnName("var");
        });

        modelBuilder.Entity<CuentaRol>(entity =>
        {
            entity.ToTable("CuentaRol");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.NombreCorto).HasMaxLength(50);
        });

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Cuenta2");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Apellidos).HasMaxLength(500);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Matricula).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(500);

            entity.HasOne(d => d.Rol).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cuenta_CuentaRol");
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.ToTable("Lote");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Fabricante).HasMaxLength(500);
            entity.Property(e => e.FechaCaducidad).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.NumLote).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.Articulo).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.ArticuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lote_CatalogoArticulos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
