using Newtonsoft.Json;

namespace Nadmin.Dto
{
    public class ArrayResultDto<T>: ResultDto
    {
        /// <summary>
        /// 结果对象集合
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public T[] Result { get; set; }
    }
}