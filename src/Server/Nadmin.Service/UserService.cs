using Nadmin.IService;
using Nadmin.Model.Models;
using System.Threading.Tasks;

namespace Nadmin.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public async Task<User> GetByUserNamePassword(string userName, string password)
        {
            return await Task.Run(() =>
                BaseEntity.AsQueryable()
                    .Where(o => o.UserName == userName && o.Password == password)
                    .First()
                );
        }
    }
}