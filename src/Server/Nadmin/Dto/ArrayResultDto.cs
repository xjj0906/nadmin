using Newtonsoft.Json;

namespace Nadmin.Dto
{
    public class ArrayResultDto<T>: ResultDto
    {
        public ArrayResultDto(T[] result, int status = 0, string msg = "ok") : base(status, msg)
        {
            Result = result;
        }

        /// <summary>
        /// 结果对象集合
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public T[] Result { get; set; }
    }
}