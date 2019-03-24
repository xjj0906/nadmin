using System.ComponentModel.DataAnnotations;

namespace Nadmin.Dto.Model.User
{
    /// <summary>
    /// 用户登录 Dto
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}