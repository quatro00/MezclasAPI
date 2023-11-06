namespace Mezclas.API.Models.DTO.Lote
{
    public class LoteDto
    {
        public Guid? Id { get; set; }

        public Guid ArticuloId { get; set; }

        public string NumLote { get; set; } = null!;

        public string Fabricante { get; set; } = null!;

        public DateTime FechaCaducidad { get; set; }
    }
}
