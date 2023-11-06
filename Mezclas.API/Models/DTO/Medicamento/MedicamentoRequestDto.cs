namespace Mezclas.API.Models.DTO.Medicamento
{
    public class MedicamentoRequestDto
    {
        public string Nombre { get; set; }
        public int UnidadMedidaId { get; set; }
        public int TipoMedicamentoId { get; set; }
        public bool? Activo { get; set; }
    }
}
