using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class TipoMovimientoInventario
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();
}
