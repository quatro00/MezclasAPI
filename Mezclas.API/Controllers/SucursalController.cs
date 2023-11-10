using Mezclas.API.Helpers;
using Mezclas.API.Models.DTO.Medicamento;
using Mezclas.API.Models.DTO.Sucursal;
using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mezclas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly ISucursalRepository sucursalRepository;

        public SucursalController(ISucursalRepository sucursalRepository)
        {
            this.sucursalRepository = sucursalRepository;
        }
        [HttpPost]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> Create(SucursalDto model)
        {

            var response = await this.sucursalRepository.Create(model, User.GetId());

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }

        [HttpPut]
        [Authorize(Roles = "Mezclas-Admin")]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] SucursalDto model)
        {

            var response = await this.sucursalRepository.Update(model, id, User.GetId());

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }

        [HttpGet]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> GetAll()
        {

            var response = await this.sucursalRepository.GetAll();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
