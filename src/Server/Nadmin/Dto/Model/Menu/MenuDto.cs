using System.Collections.Generic;

namespace Nadmin.Dto.Model.Menu
{
    /// <summary>
    /// 菜单 Dto
    /// </summary>
    public class MenuDto
    {
        //public MenuDto(string text, string i18n = null, string link = "", string externalLink = "", string icon = null, bool? group = null,
        //    bool? hideInBreadcrumb = null)
        //{
        //    Text = text;
        //    I18n = i18n;
        //    Link = link;
        //    ExternalLink = externalLink;
        //    Icon = icon;
        //    Group = group;
        //    HideInBreadcrumb = hideInBreadcrumb;
        //}

        /// <summary>
        /// 文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// i18n主键
        /// </summary>
        public string I18n { get; set; }

        /// <summary>
        /// 是否显示分组名，默认：`true`
        /// </summary>
        public bool? Group { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 外部链接
        /// </summary>
        public string ExternalLink { get; set; }

        /// <summary>
        /// 链接 target
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 隐藏面包屑，指 `page-header` 组件的自动生成面包屑时有效
        /// </summary>
        public bool? HideInBreadcrumb { get; set; }

        /// <summary>
        /// ACL配置，若导入 `@delon/acl` 时自动有效，等同于 `ACLService.can(roleOrAbility: ACLCanType)` 参数值
        /// </summary>
        public dynamic Acl { get; set; }

        /// <summary>
        /// 是否允许复用，需配合 `reuse-tab` 组件
        /// </summary>
        public bool? Reuse { get; set; }

        /// <summary>
        /// 二级菜单
        /// </summary>
        public List<MenuDto> Children { get; set; }
    }
}
