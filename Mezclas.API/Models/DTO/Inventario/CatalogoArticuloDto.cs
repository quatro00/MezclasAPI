﻿namespace Mezclas.API.Models.DTO.Inventario
{
    public class CatalogoArticuloDto
    {
        public Guid Id { get; set; }

        public string Gpo { get; set; } = null!;

        public string Gen { get; set; } = null!;

        public string Esp { get; set; } = null!;

        public string Dif { get; set; } = null!;

        public string Var { get; set; } = null!;

        public string ProgramaEspecial { get; set; } = null!;

        public string DescripcionArticuloCorta { get; set; } = null!;

        public string NivelDeAtencion { get; set; } = null!;

        public string UnidadPresentacion { get; set; } = null!;

        public decimal CantidadPresentacion { get; set; }

        public string TipoPresentacion { get; set; } = null!;

        public string CuadroBasicoSai { get; set; } = null!;

        public decimal PrecioArticulo { get; set; }

        public string PartidaPresupuestal { get; set; } = null!;
    }
}
