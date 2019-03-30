using Nadmin.Model.Models;
using System.Threading.Tasks;

namespace Nadmin.IService
{
    public interface IRoleService : IBaseService<Role>
    {
        Task<bool> CheckIsExists(string name);
    }
}