using Mezclas.API.Models;
using Mezclas.API.Models.DTO.Sucursal;

namespace Mezclas.API.Repositories.Interface
{
    public interface ISucursalRepository
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> Create(SucursalDto model, string usuario);
        Task<ResponseModel> Update(SucursalDto model, Guid id, string usuario);
    }
}
