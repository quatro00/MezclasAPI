using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class Sucursal
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Telefono2 { get; set; }

    public string CorreoElectronico { get; set; } = null!;

    public string Contacto { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<SucursalCuentum> SucursalCuenta { get; set; } = new List<SucursalCuentum>();

    public virtual ICollection<SucursalHorario> SucursalHorarios { get; set; } = new List<SucursalHorario>();
}
