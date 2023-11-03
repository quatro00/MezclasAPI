using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class Cuentum
{
    public Guid Id { get; set; }

    public string Matricula { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public Guid RolId { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public Guid UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public Guid? UsuarioModificacion { get; set; }

    public virtual CuentaRol Rol { get; set; } = null!;
}
