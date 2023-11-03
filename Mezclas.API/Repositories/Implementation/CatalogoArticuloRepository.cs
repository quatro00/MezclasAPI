using Azure.Core;
using CsvHelper;
using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.Domain;
using Mezclas.API.Models.DTO.Inventario;
using Mezclas.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Mezclas.API.Repositories.Implementation
{
    public class CatalogoArticuloRepository : ICatalogoArticuloRepository
    {
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;

        public CatalogoArticuloRepository(MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }

        public async Task<ResponseModel> ActualizaCatalogo(Stream file)
        {
            ResponseModel rm = new ResponseModel();
            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<CatalogoArticuloDto>();
            
            return rm;
        }

        public async Task<ResponseModel> GetCatalogoArticulos()
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var catalogoArticulos = await this.mezclasOncologicasDbContext.CatalogoArticulos.ToListAsync();
                List<CatalogoArticuloDto> catalogoArticuloDtos = catalogoArticulos.Select(x => new CatalogoArticuloDto()
                {
                    Id = x.Id,
                    Gpo = x.Gpo,
                    Gen = x.Gen,
                    Esp = x.Esp,
                    Dif = x.Dif,
                    Var = x.Var,
                    ProgramaEspecial = x.ProgramaEspecial,
                    DescripcionArticuloCorta = x.DescripcionArticuloCorta,
                    NivelDeAtencion = x.NivelDeAtencion,
                    UnidadPresentacion = x.UnidadPresentacion,
                    CantidadPresentacion = x.CantidadPresentacion,
                    TipoPresentacion = x.TipoPresentacion,
                    CuadroBasicoSai = x.CuadroBasicoSai,
                    PrecioArticulo = x.PrecioArticulo,
                    PartidaPresupuestal = x.PartidaPresupuestal
                }).ToList();
                rm.result = catalogoArticuloDtos;
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
