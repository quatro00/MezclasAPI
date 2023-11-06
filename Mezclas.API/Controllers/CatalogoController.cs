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
    public class CatalogoController : ControllerBase
    {
        private readonly ICatalogoRepository catalogoRepository;

        public CatalogoController(ICatalogoRepository catalogoRepository)
        {
            this.catalogoRepository = catalogoRepository;
        }

        [HttpGet("GetTipoMedicamento")]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> GetTipoMedicamento()
        {

            var response = await this.catalogoRepository.GetTipoMedicamento();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }

        [HttpGet("GetUnidadMedida")]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> GetUnidadMedida()
        {

            var response = await this.catalogoRepository.GetUnidadMedida();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
