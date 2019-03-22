using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nadmin.Common.AppSetting;
using Nadmin.Dto;
using Nadmin.Dto.Model;
using Nadmin.IService;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nadmin.Controllers
{
    [AllowAnonymous]
    public class OAuthController : BaseController
    {
        protected IOptions<AppSettings> AppSetting { get; }
        protected IUserService UserService { get; }

        public OAuthController(IOptions<AppSettings> appSetting, IUserService userService)
        {
            AppSetting = appSetting;
            UserService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = UserService.GetByUserNamePassword(userDto.UserName, userDto.Password).Result;
            if (user == null) return Ok(new ResultDto
            {
                Status = 1,
                Msg = "用户名或密码错误"
            });
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSetting.Value.JwtConfig.SecretKey);
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtClaimTypes.Audience,"api"),
                    new Claim(JwtClaimTypes.Issuer,"Nadmin"),
                    new Claim(JwtClaimTypes.Id, user.Id),
                    new Claim(JwtClaimTypes.Name, user.UserName),
                    new Claim(JwtClaimTypes.Email, user.Email??""),
                    new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber??""),
                    new Claim(JwtClaimTypes.JwtId, Guid.NewGuid().ToString())
                }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new ObjectResultDto<dynamic>
            {
                Result = new
                {
                    access_token = tokenString,
                    token_type = "Bearer",
                    profile = new
                    {
                        sid = user.Id,
                        name = user.UserName,
                        auth_time = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                        expires_at = new DateTimeOffset(expiresAt).ToUnixTimeSeconds()
                    }
                }
            });
        }
    }
}