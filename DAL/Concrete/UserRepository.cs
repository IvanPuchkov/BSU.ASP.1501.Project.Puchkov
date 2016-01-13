using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using Ninject.Infrastructure.Language;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository:IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext uow)
        {
            _context = uow;
        }

        

        public IEnumerable<DalUser> GetAll()
        {
            return _context.Set<UserLogin>().ToEnumerable().Select(user => user.ToDalUser());
        }

        public DalUser GetById(int key)
        {
            var ormuser = _context.Set<UserLogin>().FirstOrDefault(user => user.Id == key);
            
            return ormuser?.ToDalUser();
        }

       
        

        public void Create(DalUser e)
        {
            
            _context.Set<UserLogin>().Add(e.ToOrmUser());
        }

        public void Delete(DalUser e)
        {
            var user = e.ToOrmUser();
            user = _context.Set<UserLogin>().Single(u => u.Id == user.Id);
            _context.Set<UserLogin>().Remove(user);
        }

        public void Update(DalUser e)
        {
            var user = _context.Set<UserLogin>().Single(u => u.Id == e.Id);
            user.UserDisplayName = e.UserDisplayName;
            user.UserEmail = e.UserEmail;
            if (!string.IsNullOrEmpty(e.UserPassword))
            user.UserPassword = e.UserPassword;
            user.UserBanned = e.UserBanned;
            _context.Entry(user).State=EntityState.Modified;
        }

        public DalUser GetUserEntityByDisplayNameAndPassword(string displayName, string password)
        {
            var user =
                _context
                    .Set<UserLogin>()
                    .FirstOrDefault(a => (a.UserDisplayName.Equals(displayName) && (a.UserPassword.Equals(password))));
            return user?.ToDalUser();
        }

        public DalUser GetUserEntityByEmailAndPassword(string email, string password)
        {
            var user =
                _context.Set<UserLogin>()
                    .Where((a => a.UserEmail.Equals(email) && (a.UserPassword.Equals(password))))
                    .FirstOrDefault();
            return user?.ToDalUser();
        }

        public DalUser GetUserEntityByEmail(string email)
        {
            var user = _context.Set<UserLogin>().FirstOrDefault(x => x.UserEmail.Equals(email));
            return user?.ToDalUser();
        }

    }
}

