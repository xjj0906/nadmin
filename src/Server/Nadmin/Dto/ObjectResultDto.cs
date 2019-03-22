using Newtonsoft.Json;

namespace Nadmin.Dto
{
    public class ObjectResultDto<T>: ResultDto
    {
        /// <summary>
        /// 结果对象
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }
    }
}