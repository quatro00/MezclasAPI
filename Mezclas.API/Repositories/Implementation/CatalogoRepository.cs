using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.DTO.Catalogo;
using Mezclas.API.Models.DTO.Medicamento;
using Mezclas.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Repositories.Implementation
{
    public class CatalogoRepository : ICatalogoRepository
    {
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;

        public CatalogoRepository(MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }
        public async Task<ResponseModel> GetTipoMedicamento()
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var result = await mezclasOncologicasDbContext.TipoMedicamentos.Select(x => new TipoMedicamentoDto()
                {
                    Id = x.Id,
                    descripcion = x.Descripcion,
                }).ToListAsync();

                rm.result = result;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }

        public async Task<ResponseModel> GetUnidadMedida()
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var result = await mezclasOncologicasDbContext.UnidadMedida.Select(x => new UnidadMedidaDto()
                {
                    Id = x.Id,
                    descripcion = x.Descripcion,
                }).ToListAsync();

                rm.result = result;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }
    }
}
