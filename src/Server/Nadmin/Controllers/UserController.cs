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

        [HttpGet]
        public IActionResult Get(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            var pageModel = new PageModel
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var list = UserService.GetPageList(o => o.UserName.Contains(keyword) || o.Email.Contains(keyword)
                , pageModel, b => b.CreateTime, OrderByType.Desc).Result;

            var dtoList = new List<UserQueryDto>();

            foreach (var user in list)
            {
                dtoList.Add(new UserQueryDto
                {
                    UserName = user.UserName,
                    Avatar = user.Avatar,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Status = user.Status,
                    Remark = user.Remark,
                    CreateTime = user.CreateTime,
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

            User user = new User(userDto.UserName, encryptedPassword);
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Remark = userDto.Remark;
            var effectCount = UserService.Insert(user).Result;

            return Ok(new ResultDto());
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UserDto userDto)
        {
            var user = UserService.GetById(id).Result;
            if (user == null)
                return Ok(new ResultDto(2, "用户不存在"));

            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Remark = userDto.Remark;
            user.Status = userDto.Status;
            var isUpdated = UserService.Update(user).Result;

            return Ok(new ResultDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var user = UserService.GetById(id).Result;
            if (user != null)
            {
                var isDeleted = UserService.Delete(user).Result;
                return Ok(new ResultDto());
            }

            return Ok(new ResultDto());
        }
    }
}