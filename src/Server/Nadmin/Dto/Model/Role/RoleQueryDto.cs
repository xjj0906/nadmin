using System;
using System.ComponentModel.DataAnnotations;

namespace Nadmin.Dto.Model.Role
{
    public class RoleQueryDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

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