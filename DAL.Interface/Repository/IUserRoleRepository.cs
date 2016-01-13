using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IUserRoleRepository:IRepository<DalUserRole>
    {
        IEnumerable<DalRole> GetUserRoles(int userId);
        IEnumerable<DalUser> GetUsersWithRoleId(int roleId);
    }
}
