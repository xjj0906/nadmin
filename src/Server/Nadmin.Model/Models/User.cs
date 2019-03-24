using Nadmin.Model.Enum;
using SqlSugar;
using System;

namespace Nadmin.Model.Models
{
    [SugarTable("t_user")]
    public class User : BaseEntity
    {
        public User() { }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            Status = StatusEnum.Normal;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 头像路径
        /// </summary>
        [SugarColumn(Length = 255, IsNullable = true)]
        public string Avatar { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusEnum Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}