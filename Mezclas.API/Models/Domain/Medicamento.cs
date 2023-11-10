using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class Medicamento
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int TipoMedicamentoId { get; set; }

    public int UnidadMedidaId { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<EquivalenciaArticuloMedicamento> EquivalenciaArticuloMedicamentos { get; set; } = new List<EquivalenciaArticuloMedicamento>();

    public virtual TipoMedicamento TipoMedicamento { get; set; } = null!;

    public virtual UnidadMedidum UnidadMedida { get; set; } = null!;
}
