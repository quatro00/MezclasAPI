namespace Mezclas.API.Models.DTO.Inventario
{
    public class MovimientoInventarioDto
    {
        public Guid? Id { get; set; }

        public int? Folio { get; set; }

        public int TipoMovimientoInventarioId { get; set; }

        public DateTime? Fecha { get; set; }

        public int? EstatusMovimientoInventarioId { get; set; }

        public string Referencia { get; set; } = null!;
        public List<MovimientoInventarioDetDto> detalle { get; set; } = new List<MovimientoInventarioDetDto>();
    }
}
