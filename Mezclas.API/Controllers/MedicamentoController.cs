using Mezclas.API.Helpers;
using Mezclas.API.Models.DTO.Inventario;
using Mezclas.API.Models.DTO.Medicamento;
using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mezclas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        private readonly IMedicamentoRepository medicamentoRepository;

        public MedicamentoController(IMedicamentoRepository medicamentoRepository)
        {
            this.medicamentoRepository = medicamentoRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> Create(MedicamentoRequestDto model)
        {

            var response = await this.medicamentoRepository.Create(model, User.GetId());

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
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] MedicamentoRequestDto model)
        {

            var response = await this.medicamentoRepository.Update(model, id, User.GetId());

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

            var response = await this.medicamentoRepository.GetAll();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
