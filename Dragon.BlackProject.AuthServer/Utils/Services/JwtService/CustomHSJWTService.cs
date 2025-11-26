using Dragon.BlackProject.AuthServer.Utils.models;
using Dragon.BlackProject.Common.Jwt;
using Dragon.BlackProject.ModelDtos.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dragon.BlackProject.AuthServer.Utils.Services.JwtService
{
    public class CustomHSJWTService: CustomJWTService
    {
        private readonly JwtTokenOptions _jwtTokenOptions;
        public CustomHSJWTService(IOptionsMonitor<JwtTokenOptions> jwt)
        {
            if (jwt.CurrentValue == null)
            {
                throw new ArgumentException("JWT Token Options is null.\n");
            }

                _jwtTokenOptions = jwt.CurrentValue;
        }   
        public override string GenerateToken(SysUserInfo user, out string refreshToken)
        {
            if (_jwtTokenOptions.Type!=0)
            {
                throw new ArgumentException("JWT Token Options Type is not Hs256.\n");

            }
            List<Claim> claims =base.UserToClaims(user);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenOptions.SecretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtTokenOptions.Issuer,
                audience: _jwtTokenOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            refreshToken = Guid.NewGuid().ToString().Replace("-", "");
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
