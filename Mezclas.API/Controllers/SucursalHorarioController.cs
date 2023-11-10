using Mezclas.API.Helpers;
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
    public class SucursalHorarioController : ControllerBase
    {
        private readonly ISucursalHorarioRepository sucursalHorarioRepository;

        public SucursalHorarioController(ISucursalHorarioRepository sucursalHorarioRepository)
        {
            this.sucursalHorarioRepository = sucursalHorarioRepository;
        }
        [HttpPost]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> Create(SucursalHorarioDto model)
        {

            var response = await this.sucursalHorarioRepository.Create(model, User.GetId());

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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] SucursalHorarioDto model)
        {

            var response = await this.sucursalHorarioRepository.Update(model, id, User.GetId());

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

            var response = await this.sucursalHorarioRepository.GetAll();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
        [HttpGet("{sucursalId:Guid}")]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> Get([FromRoute] Guid sucursalId)
        {

            var response = await this.sucursalHorarioRepository.GetAll();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
