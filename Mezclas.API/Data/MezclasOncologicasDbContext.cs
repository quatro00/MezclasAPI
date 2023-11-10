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

    public virtual DbSet<EquivalenciaArticuloMedicamento> EquivalenciaArticuloMedicamentos { get; set; }

    public virtual DbSet<EstatusMovimientoInventario> EstatusMovimientoInventarios { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<MovimientoInventario> MovimientoInventarios { get; set; }

    public virtual DbSet<MovimientoInventarioDet> MovimientoInventarioDets { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<SucursalCuentum> SucursalCuenta { get; set; }

    public virtual DbSet<SucursalHorario> SucursalHorarios { get; set; }

    public virtual DbSet<TipoMedicamento> TipoMedicamentos { get; set; }

    public virtual DbSet<TipoMovimientoInventario> TipoMovimientoInventarios { get; set; }

    public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }

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

        modelBuilder.Entity<EquivalenciaArticuloMedicamento>(entity =>
        {
            entity.HasKey(e => new { e.ArticuloId, e.MedicamentoId });

            entity.ToTable("EquivalenciaArticuloMedicamento");

            entity.Property(e => e.CantidadPiezasUnitarias).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ContenidoPorPieza).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.Articulo).WithMany(p => p.EquivalenciaArticuloMedicamentos)
                .HasForeignKey(d => d.ArticuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquivalenciaArticuloMedicamento_CatalogoArticulos");

            entity.HasOne(d => d.Medicamento).WithMany(p => p.EquivalenciaArticuloMedicamentos)
                .HasForeignKey(d => d.MedicamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EquivalenciaArticuloMedicamento_Medicamento");
        });

        modelBuilder.Entity<EstatusMovimientoInventario>(entity =>
        {
            entity.ToTable("EstatusMovimientoInventario");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(50);
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
            entity.Property(e => e.Piezas).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.Articulo).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.ArticuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lote_CatalogoArticulos");
        });

        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.ToTable("Medicamento");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(500);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.TipoMedicamento).WithMany(p => p.Medicamentos)
                .HasForeignKey(d => d.TipoMedicamentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medicamento_TipoMedicamento");

            entity.HasOne(d => d.UnidadMedida).WithMany(p => p.Medicamentos)
                .HasForeignKey(d => d.UnidadMedidaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Medicamento_UnidadMedida");
        });

        modelBuilder.Entity<MovimientoInventario>(entity =>
        {
            entity.ToTable("MovimientoInventario");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Referencia).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacionId).HasMaxLength(50);

            entity.HasOne(d => d.EstatusMovimientoInventario).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.EstatusMovimientoInventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientoInventario_EstatusMovimientoInventario");

            entity.HasOne(d => d.TipoMovimientoInventario).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.TipoMovimientoInventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientoInventario_TipoMovimientoInventario");
        });

        modelBuilder.Entity<MovimientoInventarioDet>(entity =>
        {
            entity.ToTable("MovimientoInventarioDet");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cantidad).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.Lote).WithMany(p => p.MovimientoInventarioDets)
                .HasForeignKey(d => d.LoteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientoInventarioDet_Lote");

            entity.HasOne(d => d.MovimientoInventario).WithMany(p => p.MovimientoInventarioDets)
                .HasForeignKey(d => d.MovimientoInventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovimientoInventarioDet_MovimientoInventario");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.ToTable("Sucursal");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Contacto).HasMaxLength(500);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(500);
            entity.Property(e => e.Direccion).HasMaxLength(500);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(500);
            entity.Property(e => e.Telefono).HasMaxLength(500);
            entity.Property(e => e.Telefono2).HasMaxLength(500);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);
        });

        modelBuilder.Entity<SucursalCuentum>(entity =>
        {
            entity.HasKey(e => new { e.SucursalId, e.CuentaId });

            entity.HasOne(d => d.Cuenta).WithMany(p => p.SucursalCuenta)
                .HasForeignKey(d => d.CuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucursalCuenta_Cuenta");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.SucursalCuenta)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucursalCuenta_Sucursal");
        });

        modelBuilder.Entity<SucursalHorario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Horario");

            entity.ToTable("SucursalHorario");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.Sucursal).WithMany(p => p.SucursalHorarios)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SucursalHorario_Sucursal");
        });

        modelBuilder.Entity<TipoMedicamento>(entity =>
        {
            entity.ToTable("TipoMedicamento");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(500);
        });

        modelBuilder.Entity<TipoMovimientoInventario>(entity =>
        {
            entity.ToTable("TipoMovimientoInventario");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<UnidadMedidum>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descripcion).HasMaxLength(50);
            entity.Property(e => e.DescripcionCorta).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
