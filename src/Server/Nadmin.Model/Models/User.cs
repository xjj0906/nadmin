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
            RealName = UserName;
            Status = 0;
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            LastErrorTime = DateTime.Now;
            ErrorCount = 0;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(Length = 30, IsNullable = true)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string Password { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [SugarColumn(Length = 30, IsNullable = true)]
        public string RealName { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        [SugarColumn(Length = 30, IsNullable = true)]
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [SugarColumn(Length = 30, IsNullable = true)]
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

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        ///最后登录时间 
        /// </summary>
        public DateTime LastErrorTime { get; set; }

        /// <summary>
        ///错误次数 
        /// </summary>
        public int ErrorCount { get; set; }
    }
}