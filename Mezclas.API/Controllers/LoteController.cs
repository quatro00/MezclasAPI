using Mezclas.API.Helpers;
using Mezclas.API.Models.DTO.Lote;
using Mezclas.API.Repositories.Implementation;
using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mezclas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoteController : ControllerBase
    {
        private readonly ILoteRepository loteRepository;

        public LoteController(ILoteRepository loteRepository)
        {
            this.loteRepository = loteRepository;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> GetAll()
        {
            var response = await this.loteRepository.GetAll();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }

        [HttpGet("GetLoteByArticulo/{id:Guid}")]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> GetLoteByArticulo([FromRoute] Guid id)
        {
            var response = await this.loteRepository.GetLoteByArticulo(id);

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
        [HttpPost]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> Create(LoteDto model)
        {
            var response = await this.loteRepository.Create(model, User.GetId());

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }

    }
}
