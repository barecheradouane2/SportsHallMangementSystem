using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sportshall.Core.Entites;
using Sportshall.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class GenerateToken : IGenerateToken
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;

    public GenerateToken(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> GetAndCreateToken(AppUser user)
    {

        try
        {

            if (user == null) throw new Exception("User is null");
            if (_userManager == null) throw new Exception("UserManager is null");



            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = Encoding.ASCII.GetBytes(_configuration["Token:Secret"]);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                Issuer = _configuration["Token:Issuer"],
                SigningCredentials = credentials,
                NotBefore = DateTime.Now
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            throw new Exception("An error occurred while generating the token.", ex);
        }
       
    }
}
