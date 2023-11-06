using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class MovimientoInventario
{
    public Guid Id { get; set; }

    public int Folio { get; set; }

    public int TipoMovimientoInventarioId { get; set; }

    public DateTime Fecha { get; set; }

    public int EstatusMovimientoInventarioId { get; set; }

    public string Referencia { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacionId { get; set; }

    public virtual EstatusMovimientoInventario EstatusMovimientoInventario { get; set; } = null!;

    public virtual ICollection<MovimientoInventarioDet> MovimientoInventarioDets { get; set; } = new List<MovimientoInventarioDet>();

    public virtual TipoMovimientoInventario TipoMovimientoInventario { get; set; } = null!;
}
