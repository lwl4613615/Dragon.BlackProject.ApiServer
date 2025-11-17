using Dragon.BlackProject.AuthServer.Utils.models;
using System.Security.Claims;

namespace Dragon.BlackProject.AuthServer.Utils.Services.JwtService
{
    public abstract class CustomJWTService
    {
        public abstract string GenerateToken(User user,out string refreshToken);

        protected virtual List<Claim> UserToClaims(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name ?? string.Empty),
                new Claim(ClaimTypes.Sid,"1001"),
            };
            return claims;
        }

    }
}
