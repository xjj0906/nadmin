using Microsoft.AspNetCore.Mvc;
using Nadmin.Dto;
using Nadmin.Dto.Model.Role;
using Nadmin.IService;
using Nadmin.Model.Models;
using SqlSugar;
using System.Collections.Generic;

namespace Nadmin.Controllers
{
    public class RoleController : BaseController
    {
        protected IRoleService RoleService { get; }

        public RoleController(IRoleService roleService)
        {
            RoleService = roleService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var role = RoleService.GetById(id).Result;

            if (role == null)
                return Ok(new ResultDto(1, $"角色Id: {id} 不存在"));

            var dto = new RoleQueryDto
            {
                Id = role.Id,
                Name = role.Name,
                Remark = role.Remark,
                CreateTime = role.CreateTime,
            };

            return Ok(new ObjectResultDto<RoleQueryDto>(dto));
        }

        [HttpGet]
        public IActionResult Get(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            keyword = keyword ?? "";

            var pageModel = new PageModel
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var list = RoleService.GetPageList(o => o.Name.Contains(keyword), pageModel, b => b.CreateTime, OrderByType.Desc).Result;

            var dtoList = new List<RoleQueryDto>();

            foreach (var item in list)
            {
                dtoList.Add(new RoleQueryDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Remark = item.Remark,
                    CreateTime = item.CreateTime,
                });
            }

            return Ok(new ArrayResultDto<RoleQueryDto>(dtoList.ToArray(), pageModel));
        }

        [HttpPost]
        public IActionResult Post([FromBody]RoleDto roleDto)
        {
            if (RoleService.CheckIsExists(roleDto.Name).Result)
                return Ok(new ResultDto(1, "角色已存在"));

            Role role = new Role(roleDto.Name) { Remark = roleDto.Remark };
            RoleService.Insert(role).Wait();

            return Ok(new ResultDto());
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] RoleDto roleDto)
        {
            var role = RoleService.GetById(id).Result;
            if (role == null)
                return Ok(new ResultDto(2, "角色不存在"));

            role.Name = roleDto.Name;
            role.Remark = roleDto.Remark;
            RoleService.Update(role).Wait();

            return Ok(new ResultDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var role = RoleService.GetById(id).Result;
            if (role != null)
            {
                RoleService.Delete(role).Wait();
                return Ok(new ResultDto());
            }

            return Ok(new ResultDto());
        }
    }
}