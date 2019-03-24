using Nadmin.Common;
using Nadmin.IService;
using Nadmin.Model.Models;
using System;
using System.Threading.Tasks;

namespace Nadmin.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public async Task<User> GetByUserNamePassword(string userName, string encryptPassword)
        {
            return await Task.Run(() =>
                BaseEntity.AsQueryable()
                    .Where(o => o.UserName == userName && o.Password == encryptPassword)
                    .First()
                );
        }

        public async Task<bool> CheckUserIsExists(string userName)
        {
            return await Task.Run(() =>
                {
                    return BaseEntity.AsQueryable().Any(o =>
                        o.UserName.Equals(userName, StringComparison.CurrentCultureIgnoreCase));
                });
        }

        public async Task<bool> CheckEmailIsExists(string email)
        {
            return await Task.Run(() =>
            {
                return BaseEntity.AsQueryable().Any(o =>
                    o.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
            });
        }

        /// <summary>
        /// 对密码进行加密并返回
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string EncryptPassword(string password)
        {
            return EncryptHelper.Sha1(password);
        }
    }
}