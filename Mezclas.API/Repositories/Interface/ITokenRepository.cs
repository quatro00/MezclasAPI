using Microsoft.AspNetCore.Identity;

namespace Mezclas.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
