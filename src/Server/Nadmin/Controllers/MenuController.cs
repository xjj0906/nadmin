using Microsoft.AspNetCore.Mvc;

namespace Nadmin.Controllers
{
    public class MenuController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {


            return Ok();
        }
    }
}