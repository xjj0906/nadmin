using Newtonsoft.Json;
using SqlSugar;

namespace Nadmin.Dto
{
    public class ArrayResultDto<T> : ResultDto
    {
        public ArrayResultDto(T[] result, PageModel pageModel, int status = 0, string msg = "ok") : this(result,
            pageModel.PageIndex, pageModel.PageSize, pageModel.PageCount, status, msg)
        {
            Result = result;
        }

        public ArrayResultDto(T[] result, int pageIndex = 1, int pageSize = 0, int totalCount = -1, int status = 0, string msg = "ok") : base(status, msg)
        {
            Result = result;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        /// <summary>
        /// 结果对象集合
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public T[] Result { get; set; }
    }
}