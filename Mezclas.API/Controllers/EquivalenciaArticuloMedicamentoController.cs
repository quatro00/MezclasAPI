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
    public class EquivalenciaArticuloMedicamentoController : ControllerBase
    {
        private readonly IEquivalenciaArticuloMedicamentoRepository equvalenciaArticuloMedicamentoRepository;

        public EquivalenciaArticuloMedicamentoController(IEquivalenciaArticuloMedicamentoRepository equvalenciaArticuloMedicamentoRepository)
        {
            this.equvalenciaArticuloMedicamentoRepository = equvalenciaArticuloMedicamentoRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> Create(EquivalenciaArticuloMedicamentoRequestDto model)
        {

            var response = await this.equvalenciaArticuloMedicamentoRepository.Create(model, User.GetId());

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
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] EquivalenciaArticuloMedicamentoRequestDto model)
        {

            var response = await this.equvalenciaArticuloMedicamentoRepository.Update(model, id, User.GetId());

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

            var response = await this.equvalenciaArticuloMedicamentoRepository.GetAll();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
