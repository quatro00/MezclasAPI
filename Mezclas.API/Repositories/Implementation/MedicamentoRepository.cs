using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.Domain;
using Mezclas.API.Models.DTO.Medicamento;
using Mezclas.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Repositories.Implementation
{
    public class MedicamentoRepository : IMedicamentoRepository
    {
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;

        public MedicamentoRepository(MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }
        public async Task<ResponseModel> Create(MedicamentoRequestDto model, string usuario)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
               
                Medicamento medicamento = new Medicamento()
                {
                    Id = Guid.NewGuid(),
                    Nombre = model.Nombre,
                    TipoMedicamentoId = model.TipoMedicamentoId,
                    UnidadMedidaId = model.UnidadMedidaId,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario
                };

                await this.mezclasOncologicasDbContext.Medicamentos.AddAsync(medicamento);
                await this.mezclasOncologicasDbContext.SaveChangesAsync();

                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }

        public async Task<ResponseModel> GetAll()
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var medicamentos = await mezclasOncologicasDbContext.Medicamentos.Select(x => new MedicamentoDto()
                {
                    Id=x.Id,
                    Nombre=x.Nombre,
                    UnidadMedidaId = x.UnidadMedidaId,
                    UnidadMedida = x.UnidadMedida.Descripcion,
                    TipoMedicamentoId = x.TipoMedicamentoId,
                    TipoMedicamento = x.TipoMedicamento.Descripcion,
                    Activo = x.Activo
                }).ToListAsync();

                rm.result = medicamentos;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }

        public async Task<ResponseModel> Update(MedicamentoRequestDto model, Guid id, string usuario)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var results = await mezclasOncologicasDbContext.Medicamentos.Where(x => x.Id == id).ExecuteUpdateAsync(
                   s => s
                    .SetProperty(t => t.Nombre, t => model.Nombre)
                    .SetProperty(t => t.TipoMedicamentoId, t => model.TipoMedicamentoId)
                    .SetProperty(t => t.UnidadMedidaId, t => model.UnidadMedidaId)
                    .SetProperty(t => t.Activo, t => model.Activo)
                    );
                rm.result = results;
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
