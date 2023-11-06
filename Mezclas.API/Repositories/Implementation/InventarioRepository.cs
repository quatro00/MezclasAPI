using Mezclas.API.Data;
using Mezclas.API.Models;
using Mezclas.API.Models.Domain;
using Mezclas.API.Models.DTO.Inventario;
using Mezclas.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Mezclas.API.Repositories.Implementation
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;

        public InventarioRepository(MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }
        public async Task<ResponseModel> EntradaDeInventario(MovimientoInventarioDto model, string usuarioId)
        {
            ResponseModel rm = new ResponseModel();
            try
            {
                //colocamos los datos del movimiento
                MovimientoInventario movimientoInventario = new MovimientoInventario() {
                    Id = Guid.NewGuid(),
                    Folio = await this.mezclasOncologicasDbContext.MovimientoInventarios.CountAsync() + 1,
                    TipoMovimientoInventarioId = 1,
                    Fecha = DateTime.Now,
                    EstatusMovimientoInventarioId = 1,
                    Referencia = model.Referencia,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = usuarioId,
                    MovimientoInventarioDets = new List<MovimientoInventarioDet>()
                };

                //colocamos los detalles del movimiento
                int x = 1;
                foreach(var item in model.detalle)
                {
                    movimientoInventario.MovimientoInventarioDets.Add(new MovimientoInventarioDet()
                    {
                        Id = Guid.NewGuid(),
                        MovimientoInventarioId = movimientoInventario.Id,
                        Folio = x,
                        LoteId = item.LoteId,
                        Cantidad = item.Cantidad,
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = usuarioId,
                    });
                    x++;
                }



                await this.mezclasOncologicasDbContext.MovimientoInventarios.AddAsync(movimientoInventario);
                await this.mezclasOncologicasDbContext.SaveChangesAsync();

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
