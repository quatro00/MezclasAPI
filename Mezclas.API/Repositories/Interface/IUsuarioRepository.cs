using Mezclas.API.Models;
using Mezclas.API.Models.DTO.Usuario;

namespace Mezclas.API.Repositories.Interface
{
    public interface IUsuarioRepository
    {
        Task<ResponseModel> GetUsuarios();
        Task<ResponseModel> CreateUsuario(UsuarioDto model, string user);
    }
}
