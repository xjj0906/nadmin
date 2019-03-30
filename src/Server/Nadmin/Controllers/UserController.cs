using Microsoft.AspNetCore.Mvc;
using Nadmin.Dto;
using Nadmin.Dto.Model.User;
using Nadmin.IService;
using SqlSugar;
using System.Collections.Generic;
using User = Nadmin.Model.Models.User;

namespace Nadmin.Controllers
{
    public class UserController : BaseController
    {
        private const string InitPassword = "123456";

        protected IUserService UserService { get; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var user = UserService.GetById(id).Result;

            if (user == null)
                return Ok(new ResultDto(1, $"用户Id: {id} 不存在"));

            var dto = new UserQueryDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Avatar = user.Avatar,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status,
                Remark = user.Remark,
                CreateTime = user.CreateTime,
            };

            return Ok(new ObjectResultDto<UserQueryDto>(dto));
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

            var list = UserService.GetPageList(o => o.UserName.Contains(keyword) || o.Email.Contains(keyword)
                , pageModel, b => b.CreateTime, OrderByType.Desc).Result;

            var dtoList = new List<UserQueryDto>();

            foreach (var item in list)
            {
                dtoList.Add(new UserQueryDto
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Avatar = item.Avatar,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                    Status = item.Status,
                    Remark = item.Remark,
                    CreateTime = item.CreateTime,
                });
            }

            return Ok(new ArrayResultDto<UserQueryDto>(dtoList.ToArray(), pageModel));
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserDto userDto)
        {
            if (UserService.CheckUserIsExists(userDto.UserName).Result)
                return Ok(new ResultDto(1, "用户名已存在"));

            if (UserService.CheckEmailIsExists(userDto.Email).Result)
                return Ok(new ResultDto(1, "邮箱已存在"));

            var encryptedPassword = UserService.EncryptPassword(InitPassword);

            User user = new User(userDto.UserName, encryptedPassword)
            {
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                Remark = userDto.Remark
            };
            UserService.Insert(user).Wait();

            return Ok(new ResultDto());
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UserDto userDto)
        {
            var user = UserService.GetById(id).Result;
            if (user == null)
                return Ok(new ResultDto(2, "用户不存在"));

            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Remark = userDto.Remark;
            user.Status = userDto.Status;
            UserService.Update(user).Wait();

            return Ok(new ResultDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var user = UserService.GetById(id).Result;
            if (user != null)
            {
                if (user.UserName.ToLower() == "admin")
                {
                    return Ok(new ResultDto(3, "用户 admin 不允许删除"));
                }
                UserService.Delete(user).Wait();
                return Ok(new ResultDto());
            }

            return Ok(new ResultDto());
        }
    }
}