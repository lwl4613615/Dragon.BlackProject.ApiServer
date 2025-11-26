using Dragon.BlackProject.AuthServer.Utils.models;
using Dragon.BlackProject.ModelDtos.User;
using Dragon.BlackProject.Models.Entity;
using System.Security.Claims;

namespace Dragon.BlackProject.AuthServer.Utils.Services.JwtService
{
    public abstract class CustomJWTService
    {
        public abstract string GenerateToken(SysUserInfo user,out string refreshToken);

        protected virtual List<Claim> UserToClaims(SysUserInfo user)
        {

            //准备有效载荷
            List<Claim> claimsArray = new List<Claim>()
            {
               new Claim(ClaimTypes.Sid, user.UserId.ToString()),
               new Claim(ClaimTypes.Name, user.Name?? string.Empty),
               new Claim(ClaimTypes.MobilePhone, user.Mobile?? string.Empty),
               new Claim(ClaimTypes.OtherPhone, user.Phone?? string.Empty),
               new Claim(ClaimTypes.StreetAddress, user.Address?? string.Empty),
               new Claim(ClaimTypes.Email, user.Email?? string.Empty),
               new Claim("QQ", user.QQ.ToString()),
               new Claim("WeChat", user.WeChat?? string.Empty),
               new Claim("Sex", user.Sex.ToString())
            };
            foreach (var roleId in user.RoleIdList!)
            {
                claimsArray.Add(new Claim(ClaimTypes.Role, roleId.ToString()));
            }
            foreach (var menu in user.UserMenuList!)
            {
                claimsArray.Add(new Claim("Menus", Newtonsoft.Json.JsonConvert.SerializeObject(menu)));
            }
            foreach (var btn in user.UserBtnList!)
            {
                claimsArray.Add(new Claim("Btns", btn.Value));
            }
            return claimsArray;

        }

    }
}
