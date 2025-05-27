using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sportshall.Core.Entites;
using Sportshall.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries.Service
{
    public class GenerateToken : IGenerateToken
    {
        private readonly IConfiguration _configuration;
        public GenerateToken(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public string GetAndCreateToken(AppUser user)
        {

            List<Claim> claims = new List<Claim>
            {
                  new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
              
            };

            var security = _configuration["Token:Secret"];

            var securityKey = Encoding.ASCII.GetBytes(security);

            SigningCredentials credentials = new SigningCredentials(
                new SymmetricSecurityKey(securityKey),
                SecurityAlgorithms.HmacSha256Signature
            );


            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims
                ),

                Expires = DateTime.Now.AddDays(1),

                Issuer = _configuration["JWT:Issuer"],

                SigningCredentials = credentials,
                NotBefore = DateTime.Now,
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);




            return tokenHandler.WriteToken(token);
        }
    }
}
