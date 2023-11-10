namespace Mezclas.API.Models.DTO.Sucursal
{
    public class SucursalHorarioDto
    {
        public Guid? Id { get; set; }
        public Guid SucursalId { get; set; }
        public int Dia { get; set; }
        public string SolicitudInicio { get; set; }
        public string SolicitudTermino { get; set; }
        public string PreparacionInicio { get; set; }
        public string PreparacionTermino { get; set; }
        public string EntregaDistribuidor { get; set; }
        public string EntregaSucursal { get; set; }
        public bool? Activo { get; set; }
    }
}
