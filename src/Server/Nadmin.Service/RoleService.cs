using Nadmin.IService;
using Nadmin.Model.Models;
using System;
using System.Threading.Tasks;

namespace Nadmin.Service
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public async Task<bool> CheckIsExists(string name)
        {
            return await Task.Run(() =>
            {
                return BaseEntity.AsQueryable().Any(o =>
                    o.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            });
        }
    }
}