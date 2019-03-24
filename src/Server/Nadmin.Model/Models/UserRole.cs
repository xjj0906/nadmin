using SqlSugar;

namespace Nadmin.Model.Models
{
    [SugarTable("t_UserRole")]
    public class UserRole : BaseEntity
    {
        public UserRole() { }

        public UserRole(string userId, string roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
    }
}