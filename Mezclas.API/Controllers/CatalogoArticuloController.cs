using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mezclas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoArticuloController : ControllerBase
    {
        private readonly ICatalogoArticuloRepository catalogoArticuloRepository;

        public CatalogoArticuloController(ICatalogoArticuloRepository catalogoArticuloRepository)
        {
            this.catalogoArticuloRepository = catalogoArticuloRepository;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> GetCatalogoArticulo()
        {
            var response = await catalogoArticuloRepository.GetCatalogoArticulos();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
