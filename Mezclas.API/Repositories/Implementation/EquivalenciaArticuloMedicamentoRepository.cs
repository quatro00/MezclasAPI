using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.Domain;
using Mezclas.API.Models.DTO.Inventario;
using Mezclas.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Repositories.Implementation
{
    public class EquivalenciaArticuloMedicamentoRepository : IEquivalenciaArticuloMedicamentoRepository
    {
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;

        public EquivalenciaArticuloMedicamentoRepository(MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }
        public async Task<ResponseModel> Create(EquivalenciaArticuloMedicamentoRequestDto model, string usuario)
        {
            //validamos que no exista ya la relacion de medicamento articulo
            ResponseModel rm = new ResponseModel();
            try
            {
                EquivalenciaArticuloMedicamento equivalencia = new EquivalenciaArticuloMedicamento()
                {
                    ArticuloId = model.ArticuloId,
                    MedicamentoId = model.MedicamentoId,
                    CantidadPiezasUnitarias = model.CantidadPiezasUnitarias,
                    ContenidoPorPieza = model.ContenidoPorPieza,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario
                };

                await this.mezclasOncologicasDbContext.EquivalenciaArticuloMedicamentos.AddAsync(equivalencia);
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
                var original = await this.mezclasOncologicasDbContext.EquivalenciaArticuloMedicamentos
                    .Include(x=>x.Articulo)
                    .Include(x=>x.Medicamento).ToListAsync();
                List<EquivalenciaArticuloMedicamentoDto> result = original.Select(x => new EquivalenciaArticuloMedicamentoDto()
                {
                    ArticuloId = x.ArticuloId,
                    Articulo = $"{x.Articulo.Gpo}.{x.Articulo.Gen}.{x.Articulo.Esp}.{x.Articulo.Dif}.{x.Articulo.Var}-{x.Articulo.DescripcionArticuloCorta}",
                    MedicamentoId = x.MedicamentoId,
                    Medicamento = x.Medicamento.Nombre,
                    CantidadPiezasUnitarias = x.CantidadPiezasUnitarias,
                    ContenidoPorPieza = x.ContenidoPorPieza,

                }).ToList();
                rm.result = result;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }

        public Task<ResponseModel> Update(EquivalenciaArticuloMedicamentoRequestDto model, Guid id, string usuario)
        {
            throw new NotImplementedException();
        }
    }
}
