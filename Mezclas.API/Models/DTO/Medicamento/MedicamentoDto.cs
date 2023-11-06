namespace Mezclas.API.Models.DTO.Medicamento
{
    public class MedicamentoDto
    {
        public Guid? Id { get; set; }
        public string Nombre { get; set; }
        public int UnidadMedidaId { get; set; }
        public string? UnidadMedida { get; set; }
        public int TipoMedicamentoId { get; set; }
        public string? TipoMedicamento { get; set; }
        public bool Activo { get; set; }
    }
}
