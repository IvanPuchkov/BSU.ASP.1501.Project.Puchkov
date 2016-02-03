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
    public class RoleRepository:IRoleRepository
    {
        private readonly DbContext _context;

        public RoleRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return _context.Set<Role>().Select(role => role.ToDalRole());
        }

        public DalRole GetById(int key)
        {
            var ormRole = _context.Set<Role>().FirstOrDefault(lot => lot.Id == key);

            return ormRole?.ToDalRole();
        }

        public void Create(DalRole e)
        {

            _context.Set<Role>().Add(e.ToOrmRole());
        }

        public void Delete(DalRole e)
        {
            var role = e.ToOrmRole();
            role = _context.Set<Role>().Single(u => u.Id == role.Id);
            _context.Set<Role>().Remove(role);
        }

        public void Update(DalRole e)
        {
            var role = _context.Set<Role>().Single(u => u.Id == e.Id);
            role.UserRoleName = e.UserRoleName;
            _context.Entry(role).State = EntityState.Modified;
        }
        public DalRole GetRoleByName(string name)
        {
            var ormRole = _context.Set<Role>().FirstOrDefault(lot => lot.UserRoleName == name);
            return ormRole?.ToDalRole();
        }
    }
}
