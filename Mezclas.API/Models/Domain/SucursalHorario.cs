using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class SucursalHorario
{
    public Guid Id { get; set; }

    public Guid SucursalId { get; set; }

    public int Dia { get; set; }

    public TimeSpan SolicitudInicio { get; set; }

    public TimeSpan SolicitudTermino { get; set; }

    public TimeSpan PreparacionInicio { get; set; }

    public TimeSpan PreparacionTermino { get; set; }

    public TimeSpan EntregaDistribuidor { get; set; }

    public TimeSpan EntregaSucursal { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Sucursal Sucursal { get; set; } = null!;
}
