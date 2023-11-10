using Mezclas.API.Models.DTO.Sucursal;
using Mezclas.API.Models;

namespace Mezclas.API.Repositories.Interface
{
    public interface ISucursalHorarioRepository
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> GetBySucursalId(Guid sucursalId);
        Task<ResponseModel> Create(SucursalHorarioDto model, string usuario);
        Task<ResponseModel> Update(SucursalHorarioDto model, Guid id, string usuario);
    }
}
