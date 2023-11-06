using Mezclas.API.Models;
using Mezclas.API.Models.DTO.Inventario;

namespace Mezclas.API.Repositories.Interface
{
    public interface IInventarioRepository
    {
        Task<ResponseModel> EntradaDeInventario(MovimientoInventarioDto model, string usuarioId);
    }
}
