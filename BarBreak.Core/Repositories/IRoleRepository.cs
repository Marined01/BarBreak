using BarBreak.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarBreak.Core.Repositories
{
    public interface IRoleRepository
    {
        Role GetRoleById(int id);
        IEnumerable<Role> GetAllRoles();
        void AddRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(int id);
    }
}
