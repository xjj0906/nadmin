namespace Nadmin.Dto
{
    public class ResultDto
    {
        public ResultDto(int status = 0, string msg = "ok")
        {
            Status = status;
            Msg = msg;
        }

        /// <summary>
        /// 状态(0为正常)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
    }
}