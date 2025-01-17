using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RP.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RP.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorizationController : Controller
    {
        private readonly IConfiguration _config;

        public AuthorizationController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost(Name = "Login")]
        public IActionResult Login([FromBody] UserModel userLogin)
        {
            if (userLogin.Username == "test" && userLogin.Password == "password")
            {
                var tokenString = GenerateToken(userLogin);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
             new Claim(ClaimTypes.Name, user.Username)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Set expiration time
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
