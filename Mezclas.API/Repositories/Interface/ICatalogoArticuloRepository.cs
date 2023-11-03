using Mezclas.API.Models;

namespace Mezclas.API.Repositories.Interface
{
    public interface ICatalogoArticuloRepository
    {
        Task<ResponseModel> GetCatalogoArticulos();
        Task<ResponseModel> ActualizaCatalogo(Stream file);
    }
}
