using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using ORM;

namespace DAL.Concrete
{
    public class UserRoleRepository:IUserRoleRepository
    {
        

        private readonly DbContext _context;

        public UserRoleRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalUserRole> GetAll()
        {
            return _context.Set<UserRole>().Select(userRole => new DalUserRole
            {
                Id =userRole.Id,
                UserId = userRole.UserID,
                UserRoleId = userRole.RoleID
            });
        }

        public DalUserRole GetById(int key)
        {
            var ormUserRole = _context.Set<UserRole>().FirstOrDefault(lot => lot.Id == key);

            return ormUserRole.ToDalUserRole();
        }

        public void Create(DalUserRole e)
        {

            _context.Set<UserRole>().Add(e.ToOrmUserRole());
        }

        public void Delete(DalUserRole e)
        {
            var role = e.ToOrmUserRole();
            role = _context.Set<UserRole>().Single(u => u.Id == role.Id);
            _context.Set<UserRole>().Remove(role);
        }

        public void Update(DalUserRole e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalRole> GetUserRoles(int userId)
        {
            return _context.Set<UserRole>().Where(x => x.UserID == userId).Select(x => new DalRole {Id=x.Role.Id,UserRoleName = x.Role.UserRoleName});
        }

        public IEnumerable<DalUser> GetUsersWithRoleId(int roleId)
        {
            return _context.Set<UserRole>().Where(x => x.RoleID == roleId).Select(x => new DalUser
            {
                Id = x.UserLogin.Id,
                UserBanned = x.UserLogin.UserBanned,
                UserDisplayName = x.UserLogin.UserDisplayName,
                UserEmail = x.UserLogin.UserEmail
            });
        }
    }
}
