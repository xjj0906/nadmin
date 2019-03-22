using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Nadmin
{

    /// <summary>
    /// 产生内部服务器错误(500)结果
    /// </summary>
    public class InternalServerErrorObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Nadmin.Filter.InternalServerErrorObjectResult" /> class.
        /// </summary>
        /// <param name="value">The content to format into the entity body.</param>
        public InternalServerErrorObjectResult(object value)
            : base(value)
        {
            this.StatusCode = new int?(DefaultStatusCode);
        }
    }
}
