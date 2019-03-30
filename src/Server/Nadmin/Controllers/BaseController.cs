using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Linq;
using System.Security.Claims;

namespace Nadmin.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected Logger Logger { get; set; } = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 当前请求上下文中的用户Id
        /// </summary>
        protected string CurrentUserId
        {
            get
            {
                return HttpContext.User.Identity is ClaimsIdentity claimsIdentity
                    ? claimsIdentity.Claims.FirstOrDefault(o => o.Type == JwtClaimTypes.Id)?.Value
                    : "";
            }
        }

        /// <summary>
        /// 当前请求上下文中的用户名
        /// </summary>
        protected string CurrentUserName
        {
            get
            {
                return HttpContext.User.Identity is ClaimsIdentity claimsIdentity
                    ? claimsIdentity.Claims.FirstOrDefault(o => o.Type == JwtClaimTypes.Name)?.Value
                    : "";
            }
        }

        public BaseController()
        {

        }
    }
}