namespace Mezclas.API.Models.DTO.Inventario
{
    public class MovimientoInventarioDetDto
    {
        public Guid? Id { get; set; }

        public Guid? MovimientoInventarioId { get; set; }

        public int? Folio { get; set; }

        public Guid LoteId { get; set; }

        public decimal Cantidad { get; set; }
    }
}
