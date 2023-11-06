using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class UnidadMedidum
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string DescripcionCorta { get; set; } = null!;

    public bool Activo { get; set; }

    public virtual ICollection<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
}
