using E_Commerce.infrastructure.services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Texon.Domin.Entities.Auth;
using Texon.Service.Contracts;

namespace Texon.infrastructure.Service
{
    public class TokenService(IOptions<JwtOptions> options) : ITokenService
    {
        public string GetToken(ApplicationUser user, IList<string> roles)
        {

            var jwt = options.Value;
            

            //1
            List<Claim> claims = new List<Claim>

            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Email , user.Email),
                new Claim(JwtRegisteredClaimNames.Name , user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub , user.Id),
                
            };


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //2
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperDuperSecretKeyMySuperDuperSecretKeyMySuperDuperSecretKey"));
           
            //3

            var credentails = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            
            //4

            var token = new JwtSecurityToken(claims: claims,
               issuer: jwt.issure,// "yourdomain.com",
               audience: jwt.audience,//"yourdomain.com",
               expires: DateTime.Now.AddDays(jwt.Duration)
               , signingCredentials: credentails

               );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}
