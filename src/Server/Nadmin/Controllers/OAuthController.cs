using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityModel;
using Microsoft.Extensions.Options;
using Nadmin.Model;

namespace Nadmin.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class OAuthController : ControllerBase
    {
        protected IOptions<JwtConfig> JwtConfig { get; }

        public OAuthController(IOptions<JwtConfig> jwtConfig)
        {
            JwtConfig = jwtConfig;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            //var user = _store.FindUser(userDto.UserName, userDto.Password);
            var user = new User
            {
                Id = 1,
                Name = "JJ",
                Email = "JJ@JJ.com",
                PhoneNumber = "12345678945",
            };
            if (user == null) return Unauthorized();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtConfig.Value.SecretKey);
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer,"http://localhost:5200"),
                    new Claim(JwtClaimTypes.Id, user.Id.ToString()),
                    new Claim(JwtClaimTypes.Name, user.Name),
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
                    new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString())
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                msg = "ok",
                access_token = tokenString,
                token_type = "Bearer",
                profile = new
                {
                    sid = user.Id,
                    name = user.Name,
                    auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                    expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                }
            });
        }
    }

    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}