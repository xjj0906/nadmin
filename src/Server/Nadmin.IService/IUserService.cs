using System.Threading.Tasks;
using Nadmin.Model.Models;

namespace Nadmin.IService
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetByUserNamePassword(string userName,string password);
    }
}