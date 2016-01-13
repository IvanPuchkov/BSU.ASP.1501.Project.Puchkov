using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        UserEntity GetUserEntity(int id);
        IEnumerable<UserEntity> GetAllUserEntities();
        void CreateUser(UserEntity user);
        void DeleteUser(UserEntity user);
        void UpdateUser(UserEntity user);
        UserEntity GetUserEntityByDisplayNameAndPassword(string displayName, string password);
        UserEntity GetUserEntityByEmailAndPassword(string displayName, string password);
        UserEntity GetUserEntityByEmail(string email);
    }
}
