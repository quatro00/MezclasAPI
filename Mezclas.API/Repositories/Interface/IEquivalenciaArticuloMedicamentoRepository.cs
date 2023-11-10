using Mezclas.API.Models;
using Mezclas.API.Models.DTO.Inventario;

namespace Mezclas.API.Repositories.Interface
{
    public interface IEquivalenciaArticuloMedicamentoRepository
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> Create(EquivalenciaArticuloMedicamentoRequestDto model, string usuario);
        Task<ResponseModel> Update(EquivalenciaArticuloMedicamentoRequestDto model, Guid id, string usuario);
    }
}
