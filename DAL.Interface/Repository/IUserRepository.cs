using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IUserRepository:IRepository<DalUser>
    {
        DalUser GetUserEntityByDisplayNameAndPassword(string displayName,string password);
        DalUser GetUserEntityByEmailAndPassword(string email, string password);
        DalUser GetUserEntityByEmail(string email);
    }
}
