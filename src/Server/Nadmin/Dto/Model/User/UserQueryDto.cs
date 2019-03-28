using Nadmin.Model.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace Nadmin.Dto.Model.User
{
    /// <summary>
    /// 用户查询 Dto
    /// </summary>
    public class UserQueryDto
    {
        // <summary>
        /// 主键
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        public string Avatar { get; set; }

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

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}