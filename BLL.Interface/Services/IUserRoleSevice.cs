using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserRoleSevice
    {
        UserRoleEntity GetUserRoleEntityById(int id);
        IEnumerable<UserRoleEntity> GetAllUserRoleEntities();
        IEnumerable<RoleEntity> GetUserRoles(int userId);
        IEnumerable<UserEntity> GetUsersWithRoleId(int roleId);
        void AddUserRoleEntity(UserRoleEntity role);
        void DeleteUserRoleEntity(UserRoleEntity role);
    }
}
