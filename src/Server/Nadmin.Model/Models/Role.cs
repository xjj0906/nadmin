using SqlSugar;
using System;
using System.Collections.Generic;

namespace Nadmin.Model.Models
{
    [SugarTable("t_Role")]
    public class Role : BaseEntity
    {
        public Role()
        {

        }

        public Role(string name)
        {
            Name = name;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

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
        /// 所属此角色的用户
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<User> Users { get; set; }
    }
}