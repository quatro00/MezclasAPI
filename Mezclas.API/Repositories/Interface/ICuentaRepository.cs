using Mezclas.API.Models;
using Mezclas.API.Models.DTO.Cuenta;

namespace Mezclas.API.Repositories.Interface
{
    public interface ICuentaRepository
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> AsociarCuentaSucursal(SucursalCuentaDto model);
    }
}
