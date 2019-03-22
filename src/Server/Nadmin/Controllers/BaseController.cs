using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Nadmin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected Logger Logger { get; set; } = LogManager.GetCurrentClassLogger();

        public BaseController()
        {

        }
    }
}