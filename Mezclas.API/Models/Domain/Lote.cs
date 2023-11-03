using System;
using System.Collections.Generic;

namespace Mezclas.API.Models.Domain;

public partial class Lote
{
    public Guid Id { get; set; }

    public Guid ArticuloId { get; set; }

    public string NumLote { get; set; } = null!;

    public string Fabricante { get; set; } = null!;

    public DateTime FechaCaducidad { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string UsuarioCreacion { get; set; } = null!;

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual CatalogoArticulo Articulo { get; set; } = null!;
}
