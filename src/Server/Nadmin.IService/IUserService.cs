using Nadmin.Model.Models;
using System.Threading.Tasks;

namespace Nadmin.IService
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetByUserNamePassword(string userName, string password);
        Task<bool> CheckUserIsExists(string userName);
        Task<bool> CheckEmailIsExists(string email);
        string EncryptPassword(string password);
    }
}