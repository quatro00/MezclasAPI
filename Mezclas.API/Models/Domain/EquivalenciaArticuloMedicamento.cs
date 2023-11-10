using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class EquivalenciaArticuloMedicamento
{
    public Guid ArticuloId { get; set; }

    public Guid MedicamentoId { get; set; }

    public decimal CantidadPiezasUnitarias { get; set; }

    public decimal ContenidoPorPieza { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual CatalogoArticulo Articulo { get; set; } = null!;

    public virtual Medicamento Medicamento { get; set; } = null!;
}
