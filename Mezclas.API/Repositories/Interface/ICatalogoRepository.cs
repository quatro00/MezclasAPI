using Mezclas.API.Models;

namespace Mezclas.API.Repositories.Interface
{
    public interface ICatalogoRepository
    {
        Task<ResponseModel> GetUnidadMedida();
        Task<ResponseModel> GetTipoMedicamento();
    }
}
