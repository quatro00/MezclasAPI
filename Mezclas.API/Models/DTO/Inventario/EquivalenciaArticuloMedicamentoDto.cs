namespace Mezclas.API.Models.DTO.Inventario
{
    public class EquivalenciaArticuloMedicamentoDto
    {
        public Guid ArticuloId { get; set; }
        public string Articulo { get; set; } = "";
        public Guid MedicamentoId { get; set; }
        public string Medicamento { get; set; } = "";
        public decimal CantidadPiezasUnitarias { get; set; }
        public decimal ContenidoPorPieza { get; set; }
    }
}
