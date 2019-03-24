using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nadmin.Common;
using Nadmin.Dto;
using Nadmin.Model;

namespace Nadmin.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class DataBaseController : BaseController
    {
        [HttpGet("init")]
        public ActionResult<ResultDto> Init()
        {
            var dbClient = DataBaseClient.Create();
            DataBaseManager.CreateOrUpdateTableSchemas(dbClient);
            return new ResultDto
            {
                Status = 0,
                Msg = "数据库初始化成功",
            };
        }
    }
}