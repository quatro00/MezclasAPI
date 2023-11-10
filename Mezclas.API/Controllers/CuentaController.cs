using Mezclas.API.Helpers;
using Mezclas.API.Models.DTO.Cuenta;
using Mezclas.API.Models.DTO.Inventario;
using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mezclas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaRepository cuentaRepository;
        public CuentaController(ICuentaRepository cuentaRepository)
        {
            this.cuentaRepository = cuentaRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> GetAll()
        {

            var response = await this.cuentaRepository.GetAll();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }

        [HttpPost("AsociarSucursalCuenta")]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> AsociarSucursalCuenta(SucursalCuentaDto model)
        {

            var response = await this.cuentaRepository.AsociarCuentaSucursal(model);

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
