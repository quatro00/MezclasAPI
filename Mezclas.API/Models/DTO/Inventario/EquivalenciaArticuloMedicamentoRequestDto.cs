namespace Mezclas.API.Models.DTO.Inventario
{
    public class EquivalenciaArticuloMedicamentoRequestDto
    {
        public Guid ArticuloId { get; set; }
        public Guid MedicamentoId { get; set; }
        public decimal CantidadPiezasUnitarias { get; set; }
        public decimal ContenidoPorPieza { get; set; }

    }
}
