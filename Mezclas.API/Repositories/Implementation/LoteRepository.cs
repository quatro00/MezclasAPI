using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.Domain;
using Mezclas.API.Models.DTO.Inventario;
using Mezclas.API.Models.DTO.Lote;
using Mezclas.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Repositories.Implementation
{
    public class LoteRepository : ILoteRepository
    {
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;

        public LoteRepository(MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }
        public async Task<ResponseModel> Create(LoteDto model, string usuario)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                //validamos que el numero de lote no se haya registrado
                if(mezclasOncologicasDbContext.Lotes.Where(x=>x.NumLote == model.NumLote).Count() > 0)
                {
                    rm.SetResponse(false, $"El lote numero: {model.NumLote} ya se encuentra registrado.");
                    return rm;
                }
                //validamos que la fecha sea posterior a la fecha de hoy
                if(model.FechaCaducidad <= DateTime.Now)
                {
                    rm.SetResponse(false, $"La fecha de caducidad no puede ser menor a la fecha actual.");
                    return rm;
                }
                Lote lote = new Lote()
                {
                    Id = Guid.NewGuid(),
                    ArticuloId = model.ArticuloId,
                    NumLote = model.NumLote,
                    Fabricante = model.Fabricante,
                    FechaCaducidad = model.FechaCaducidad,
                    Piezas = 0,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario
                };

                await this.mezclasOncologicasDbContext.Lotes.AddAsync(lote);
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
                var lotes = await this.mezclasOncologicasDbContext.Lotes.ToListAsync();
                var lotesDto = lotes.Select(x => new LoteDto()
                {
                    Id = x.Id,
                   ArticuloId=x.ArticuloId,
                   NumLote = x.NumLote,
                   Fabricante = x.Fabricante,
                   FechaCaducidad = x.FechaCaducidad
                }).ToList();

                rm.result = lotesDto;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }

        public async Task<ResponseModel> GetLoteByArticulo(Guid articulo)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var lotes = await this.mezclasOncologicasDbContext.Lotes.ToListAsync();
                var lotesDto = lotes.Select(x => new LoteDto()
                {
                    Id = x.Id,
                    ArticuloId = x.ArticuloId,
                    NumLote = x.NumLote,
                    Fabricante = x.Fabricante,
                    FechaCaducidad = x.FechaCaducidad
                }).Where(x=>x.ArticuloId == articulo).ToList();

                rm.result = lotesDto;
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
