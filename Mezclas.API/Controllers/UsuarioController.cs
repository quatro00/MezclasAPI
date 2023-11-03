using Mezclas.API.Models;
using Mezclas.API.Helpers;
using Mezclas.API.Models.DTO.Usuario;
using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mezclas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> GetAll()
        {
            var response = await usuarioRepository.GetUsuarios();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create(UsuarioDto request)
        {
            var response = await usuarioRepository.CreateUsuario(request, User.GetId());

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
