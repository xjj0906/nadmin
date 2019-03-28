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
                    Text = "系统管理",
                    Group = true,
                    HideInBreadcrumb = true,
                    Children = new List<MenuDto>
                    {
                        new MenuDto
                        {
                            Text = "用户管理",
                            Icon = "anticon anticon-user",
                            Link = "/sys/user"
                        },
                        new MenuDto
                        {
                            Text = "角色管理",
                            Icon = "anticon anticon-team",
                            Link = "/sys/role"
                        },
                    }
                }
            };

            return Ok(menus);
        }
    }
}