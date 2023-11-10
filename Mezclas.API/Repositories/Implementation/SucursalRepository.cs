using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.Domain;
using Mezclas.API.Models.DTO.Sucursal;
using Mezclas.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Repositories.Implementation
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;
        public SucursalRepository(MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }
        public async Task<ResponseModel> Create(SucursalDto model, string usuario)
        {
            ResponseModel rm = new ResponseModel();
            try
            {

                Sucursal sucursal = new Sucursal()
                {
                    Id = Guid.NewGuid(),
                    Nombre = model.Nombre,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    Telefono2 = model.Telefono2,
                    CorreoElectronico = model.CorreoElectronico,
                    Contacto = model.Contacto,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuario
                };

                await this.mezclasOncologicasDbContext.Sucursals.AddAsync(sucursal);
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
                var medicamentos = await mezclasOncologicasDbContext.Sucursals.Select(x => new SucursalDto()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Direccion = x.Direccion,
                    Telefono = x.Telefono,
                    Telefono2 = x.Telefono2,
                    Contacto = x.Contacto,
                    CorreoElectronico = x.CorreoElectronico,
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

        public async Task<ResponseModel> Update(SucursalDto model, Guid id, string usuario)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var results = await mezclasOncologicasDbContext.Sucursals.Where(x => x.Id == id).ExecuteUpdateAsync(
                   s => s
                    .SetProperty(t => t.Nombre, t => model.Nombre)
                    .SetProperty(t => t.Direccion, t => model.Direccion)
                    .SetProperty(t => t.Telefono, t => model.Telefono)
                    .SetProperty(t => t.Telefono2, t => model.Telefono2)
                    .SetProperty(t => t.CorreoElectronico, t => model.CorreoElectronico)
                    .SetProperty(t => t.Contacto, t => model.Contacto)
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
