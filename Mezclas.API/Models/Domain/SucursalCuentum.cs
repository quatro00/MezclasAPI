using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class SucursalCuentum
{
    public Guid SucursalId { get; set; }

    public Guid CuentaId { get; set; }

    public bool Activo { get; set; }

    public virtual Cuentum Cuenta { get; set; } = null!;

    public virtual Sucursal Sucursal { get; set; } = null!;
}
