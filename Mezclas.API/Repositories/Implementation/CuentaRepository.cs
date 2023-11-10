using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.Domain;
using Mezclas.API.Models.DTO;
using Mezclas.API.Models.DTO.Cuenta;
using Mezclas.API.Models.DTO.Medicamento;
using Mezclas.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Repositories.Implementation
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;
        public CuentaRepository(MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }

        public async Task<ResponseModel> AsociarCuentaSucursal(SucursalCuentaDto model)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                if(this.mezclasOncologicasDbContext.SucursalCuenta.Where(x=>x.SucursalId == model.sucursalId && x.CuentaId == model.cuentaId).Count() > 0)
                {
                    rm.SetResponse(false, "la cuenta ya esta asociada a la sucursal.");
                    return rm;
                }
                SucursalCuentum item = new SucursalCuentum() {
                    SucursalId = model.sucursalId,
                    CuentaId = model.cuentaId,
                    Activo = true,
                };

                await this.mezclasOncologicasDbContext.SucursalCuenta.AddRangeAsync(item);
                await this.mezclasOncologicasDbContext.SaveChangesAsync();

                rm.SetResponse(true, "Datos guardados con exito.");
            }
            catch (Exception ex)
            {
                rm.SetResponse(false, "Ocurrio un error inesperado.");
            }
            return rm;
        }

        public async Task<ResponseModel> BorrarCuentaSucursal(SucursalCuentaDto model)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var result = await mezclasOncologicasDbContext.SucursalCuenta.Where(x => x.SucursalId == model.sucursalId && x.CuentaId == model.cuentaId).ExecuteDeleteAsync();
                  
                rm.result = result;
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
                var medicamentos = await mezclasOncologicasDbContext.Cuenta.Select(x => new CuentaDto()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Matricula = x.Matricula,
                    Apellidos = x.Apellidos,
                    CorreoElectronico = x.CorreoElectronico,
                    RolId = x.RolId,
                    Rol = x.Rol.Descripcion,
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

        public async Task<ResponseModel> GetCuentasBySucursal(Guid sucursalId)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                var medicamentos = await mezclasOncologicasDbContext.Cuenta
                    .Where(x=>x.SucursalCuenta.Any(x=>x.SucursalId == sucursalId)).Select(x => new CuentaDto()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Matricula = x.Matricula,
                    Apellidos = x.Apellidos,
                    CorreoElectronico = x.CorreoElectronico,
                    RolId = x.RolId,
                    Rol = x.Rol.Descripcion,
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
    }
}
