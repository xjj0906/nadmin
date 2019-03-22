using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Nadmin.Dto;
using NLog;

namespace Nadmin.Filter
{

    /// <summary>
    /// 全局异常错误日志
    /// </summary>
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        protected Logger Logger { get; set; } = LogManager.GetCurrentClassLogger();
        public GlobalExceptionsFilter(IHostingEnvironment env)
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            context.Result = new InternalServerErrorObjectResult(new ResultDto
            {
                Status = 500,
                Msg = context.Exception.Message,
            });

            Logger.Error(context.Exception);
        }
    }
}
