namespace Nadmin.Dto
{
    public class ResultDto
    {
        /// <summary>
        /// 状态(0为正常)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; } = "ok";
    }
}