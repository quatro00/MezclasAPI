namespace Mezclas.API.Models.DTO.Cuenta
{
    public class CuentaDto
    {
        public Guid Id { get; set; }

        public string Matricula { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public Guid RolId { get; set; }
        public string Rol { get; set; }

        public bool Activo { get; set; }
    }
}
