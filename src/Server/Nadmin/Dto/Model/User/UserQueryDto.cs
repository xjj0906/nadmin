using System.ComponentModel.DataAnnotations;
using Nadmin.Model.Enum;

namespace Nadmin.Dto.Model.User
{
    /// <summary>
    /// 用户 Dto
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required]
        public StatusEnum Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}