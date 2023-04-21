using CDPHE.H20.Data.ViewModels;
using CDPHE.H20.Services;
using CDPHE.H20.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CDPHE.H20.WebAPI.Controllers
{
    [Route("v1/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        { 
            _configuration= configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken(UserRole userRole)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            if (!String.IsNullOrEmpty(userRole.Email))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", userRole.Id.ToString()),
                    new Claim("Email", userRole.Email),
                    new Claim("FirstName", userRole.FirstName),
                    new Claim("LastName", userRole.LastName),
                    new Claim(ClaimTypes.Role, userRole.Role)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials:signIn
                    );

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }

            return BadRequest("Invalid Credentials");
        }
    }
}
