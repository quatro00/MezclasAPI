using Mezclas.API.Models;
using Mezclas.API.Models.DTO.Medicamento;

namespace Mezclas.API.Repositories.Interface
{
    public interface IMedicamentoRepository
    {
        Task<ResponseModel> GetAll();
        Task<ResponseModel> Create(MedicamentoRequestDto model, string usuario);
        Task<ResponseModel> Update(MedicamentoRequestDto model, Guid id, string usuario);
    }
}
