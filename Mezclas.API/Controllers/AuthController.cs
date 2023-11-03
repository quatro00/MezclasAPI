using Mezclas.API.Models.DTO.Auth;
using Mezclas.API.Repositories.Implementation;
using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Mezclas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, IUsuarioRepository usuarioRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            //checamos el email
            var identityUser = await userManager.FindByNameAsync(request.username);

            if (identityUser is not null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(identityUser, request.password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);

                    if (roles.Where(x=>x.Contains("Mezclas")).Count() > 0)
                    {
                        var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());
                        var response = new LoginResponseDto()
                        {
                            Email = identityUser.Email,
                            Roles = roles.ToList(),
                            Token = jwtToken,
                            Nombre = "Juan",
                            Apellidos = "Perez",
                            Username = identityUser.UserName
                        };

                        return Ok(response);
                    }
                }

            }

            ModelState.AddModelError("error", "Email o password incorrecto.");
            return ValidationProblem(ModelState);
        }

        [HttpGet("GetUsuarios")]
        [Authorize(Roles = "Mezclas-Admin")]
        public async Task<IActionResult> GetUsuarios()
        {
            var response = await this.usuarioRepository.GetUsuarios();

            if (!response.response)
            {
                ModelState.AddModelError("error", response.message);
                return ValidationProblem(ModelState);
            }

            return Ok(response.result);
        }
    }
}
