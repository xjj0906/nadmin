using Microsoft.AspNetCore.Mvc;
using Nadmin.Dto;
using Nadmin.Dto.Model;
using Nadmin.Dto.Model.User;
using Nadmin.IService;
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