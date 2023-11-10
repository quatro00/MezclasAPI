using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.Domain;
using Mezclas.API.Models.DTO.Medicamento;
using Mezclas.API.Models.DTO.Sucursal;
using Mezclas.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Repositories.Implementation
{
    public class SucursalHorarioRepository : ISucursalHorarioRepository
    {
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;

        public SucursalHorarioRepository(MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }
        public async Task<ResponseModel> Create(SucursalHorarioDto model, string usuario)
        {
            ResponseModel rm = new ResponseModel();
            try
            {

                SucursalHorario entity = new SucursalHorario()
                {
                    Id = Guid.NewGuid(),
                    SucursalId = model.SucursalId,
                    Dia = model.Dia,
                    SolicitudInicio = new TimeSpan(Int32.Parse(model.SolicitudInicio.Split(" ")[4].Split(":")[0]), Int32.Parse(model.SolicitudInicio.Split(" ")[4].Split(":")[1]), 0),
                    SolicitudTermino = new TimeSpan(Int32.Parse(model.SolicitudTermino.Split(" ")[4].Split(":")[0]), Int32.Parse(model.SolicitudTermino.Split(" ")[4].Split(":")[1]), 0),
                    PreparacionInicio = new TimeSpan(Int32.Parse(model.PreparacionInicio.Split(" ")[4].Split(":")[0]), Int32.Parse(model.PreparacionInicio.Split(" ")[4].Split(":")[1]), 0),
                    PreparacionTermino = new TimeSpan(Int32.Parse(model.PreparacionTermino.Split(" ")[4].Split(":")[0]), Int32.Parse(model.PreparacionTermino.Split(" ")[4].Split(":")[0]), 0),
                    EntregaDistribuidor = new TimeSpan(Int32.Parse(model.EntregaDistribuidor.Split(" ")[4].Split(":")[0]), Int32.Parse(model.EntregaDistribuidor.Split(" ")[4].Split(":")[0]), 0),
                    EntregaSucursal = new TimeSpan(Int32.Parse(model.EntregaSucursal.Split(" ")[4].Split(":")[0]), Int32.Parse(model.EntregaSucursal.Split(" ")[4].Split(":")[0]), 0),

                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario
                };

                await this.mezclasOncologicasDbContext.SucursalHorarios.AddAsync(entity);
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
                var r2 = await mezclasOncologicasDbContext.SucursalHorarios.ToListAsync();
                var response = await mezclasOncologicasDbContext.SucursalHorarios.Select(x => new SucursalHorarioDto()
                {
                    Id = x.Id,
                    SucursalId = x.SucursalId,
                    Dia = x.Dia,
                    SolicitudInicio = new DateTime().Add(x.SolicitudInicio).ToString("hh:mm tt"),
                    SolicitudTermino = new DateTime().Add(x.SolicitudTermino).ToString("hh:mm tt"),
                    PreparacionInicio = new DateTime().Add(x.PreparacionInicio).ToString("hh:mm tt"),
                    PreparacionTermino = new DateTime().Add(x.PreparacionTermino).ToString("hh:mm tt"),
                    EntregaDistribuidor = new DateTime().Add(x.EntregaDistribuidor).ToString("hh:mm tt"),
                    EntregaSucursal = new DateTime().Add(x.EntregaSucursal).ToString("hh:mm tt"),
                    Activo = x.Activo
                }).ToListAsync();

                rm.result = response;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }

        public async Task<ResponseModel> GetBySucursalId(Guid sucursalId)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var response = await mezclasOncologicasDbContext.SucursalHorarios.Select(x => new SucursalHorarioDto()
                {
                    Id = x.Id,
                    SucursalId = x.SucursalId,
                    Dia = x.Dia,
                    SolicitudTermino = x.SolicitudTermino.ToString("hh:mm tt"),
                    PreparacionInicio = x.PreparacionInicio.ToString("hh:mm tt"),
                    PreparacionTermino = x.PreparacionTermino.ToString("hh:mm tt"),
                    EntregaDistribuidor = x.EntregaDistribuidor.ToString("hh:mm tt"),
                    EntregaSucursal = x.EntregaSucursal.ToString("hh:mm tt"),
                    Activo = x.Activo
                }).Where(x=>x.SucursalId == sucursalId).ToListAsync();

                rm.result = response;
                rm.SetResponse(true, "Datos guardados con éxito.");

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }

        public async Task<ResponseModel> Update(SucursalHorarioDto model, Guid id, string usuario)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                /*
                var results = await mezclasOncologicasDbContext.SucursalHorarios.Where(x => x.Id == id).ExecuteUpdateAsync(
                   s => s
                    .SetProperty(t => t.Dia, t => model.Dia)
                    .SetProperty(t => t.SolicitudInicio, t => model.SolicitudInicio)
                    .SetProperty(t => t.SolicitudTermino, t => model.SolicitudTermino)
                    .SetProperty(t => t.PreparacionInicio, t => model.PreparacionInicio)
                    .SetProperty(t => t.PreparacionTermino, t => model.PreparacionTermino)
                    .SetProperty(t => t.EntregaDistribuidor, t => model.EntregaDistribuidor)
                    .SetProperty(t => t.EntregaSucursal, t => model.EntregaSucursal)
                    .SetProperty(t => t.Activo, t => model.Activo)
                    );
                rm.result = results;
                rm.SetResponse(true, "Datos guardados con éxito.");
                */

            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }
    }
}
