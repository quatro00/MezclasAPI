using Mezclas.API.Data;
using Mezclas.API.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mezclas.API.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        private readonly MezclasOncologicasDbContext mezclasOncologicasDbContext;

        public TokenRepository(IConfiguration configuration, MezclasOncologicasDbContext mezclasOncologicasDbContext)
        {
            this.configuration = configuration;
            this.mezclasOncologicasDbContext = mezclasOncologicasDbContext;
        }

        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //Buscamos el usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
