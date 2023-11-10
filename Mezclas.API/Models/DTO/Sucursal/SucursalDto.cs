namespace Mezclas.API.Models.DTO.Sucursal
{
    public class SucursalDto
    {
        public Guid? Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        public string CorreoElectronico { get; set; }
        public string Contacto { get; set; }
        public bool? Activo { get; set; }
    }
}
