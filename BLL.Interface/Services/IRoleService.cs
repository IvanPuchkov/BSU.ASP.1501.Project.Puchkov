using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IRoleService
    {
        RoleEntity GetRoleByName(string name);
        RoleEntity GetRoleById(int id);
        void AddRoleEntity(RoleEntity userRoleEntity);
        void DeleteRoleEntity(RoleEntity userRoleEntity);
        void UpdateRoleEntity(RoleEntity userRoleEntity);
        IEnumerable<RoleEntity> GetAllRoles();
    }
}
