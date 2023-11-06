using Mezclas.API.Models;
using Mezclas.API.Models.DTO.Lote;

namespace Mezclas.API.Repositories.Interface
{
    public interface ILoteRepository
    {
        Task<ResponseModel> Create(LoteDto model, string usuario);
        Task<ResponseModel> GetLoteByArticulo(Guid articulo);
        Task<ResponseModel> GetAll();
    }
}
