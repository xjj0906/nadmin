using System;
using System.ComponentModel.DataAnnotations;

namespace Nadmin.Dto.Model.Role
{
    public class RoleDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}