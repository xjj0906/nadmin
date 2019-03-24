using Newtonsoft.Json;

namespace Nadmin.Dto
{
    public class ObjectResultDto<T> : ResultDto
    {
        public ObjectResultDto(T result, int status = 0, string msg = "ok") : base(status, msg)
        {
            Result = result;
        }

        /// <summary>
        /// 结果对象
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public T Result { get; set; }
    }
}