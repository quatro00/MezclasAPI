using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class CuentaRol
{
    public Guid Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public string NombreCorto { get; set; } = null!;

    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
