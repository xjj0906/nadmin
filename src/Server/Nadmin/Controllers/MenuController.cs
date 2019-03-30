using Microsoft.AspNetCore.Mvc;
using Nadmin.Dto.Model.Menu;
using System.Collections.Generic;

namespace Nadmin.Controllers
{
    public class MenuController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            var menus = new List<MenuDto>
            {
                new MenuDto
                {
                    Text = "Nadmin",
                    Group = false,
                    HideInBreadcrumb = true,
                    Children = new List<MenuDto>
                    {
                        new MenuDto
                        {
                            Text = "首页",
                            Icon = "anticon anticon-home",
                            Link = "/"
                        },
                        new MenuDto
                        {
                            Text = "系统管理",
                            Icon = "anticon anticon-setting",
                            Children = new List<MenuDto>
                            {
                                new MenuDto
                                {
                                    Text = "用户管理",
                                    //Icon = "anticon anticon-user",//二级菜单无效
                                    Link = "/sys/user"
                                },
                                new MenuDto
                                {
                                    Text = "角色管理",
                                    //Icon = "anticon anticon-team",//二级菜单无效
                                    Link = "/sys/role"
                                },
                            }
                        }
                    }
                },
            };

            return Ok(menus);
        }
    }
}