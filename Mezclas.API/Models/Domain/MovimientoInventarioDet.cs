using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class MovimientoInventarioDet
{
    public Guid Id { get; set; }

    public Guid MovimientoInventarioId { get; set; }

    public int Folio { get; set; }

    public Guid LoteId { get; set; }

    public decimal Cantidad { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Lote Lote { get; set; } = null!;

    public virtual MovimientoInventario MovimientoInventario { get; set; } = null!;
}
