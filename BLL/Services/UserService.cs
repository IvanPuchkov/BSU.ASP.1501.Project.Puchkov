using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Services;
using BLL.Interface.Entities;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class UserService:IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this._uow = uow;
            _userRepository = repository;
        }

        public void CreateUser(UserEntity user)
        {
            user.UserPassword = EncodePassword(user.UserPassword);
            _userRepository.Create(user.ToDalUser());
            _uow.Commit();
        }

        public UserEntity GetUserEntity(int id)
        {
            return _userRepository.GetById(id)?.ToBllUserEntity();
        }

        public void DeleteUser(UserEntity user)
        {
            _userRepository.Delete(user.ToDalUser());
            _uow.Commit();
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            return _userRepository.GetAll().Select(user => user.ToBllUserEntity());
        }

        private string EncodePassword(string password)
        {
            byte[] bytearray = Encoding.UTF8.GetBytes(password);
            bytearray = new System.Security.Cryptography.SHA256Managed().ComputeHash(bytearray);
            return Encoding.Unicode.GetString(bytearray);
        }

        public UserEntity GetUserEntityByDisplayNameAndPassword(string displayName, string password)
        {
            return
                _userRepository.GetUserEntityByDisplayNameAndPassword(displayName, EncodePassword(password))?
                    .ToBllUserEntity();
        }

        public UserEntity GetUserEntityByEmailAndPassword(string email, string password)
        {
            return _userRepository.GetUserEntityByEmailAndPassword(email, EncodePassword(password))?.ToBllUserEntity();
        }

        public void UpdateUser(UserEntity user)
        {
            _userRepository.Update(user.ToDalUser());
            _uow.Commit();
        }

        public UserEntity GetUserEntityByEmail(string email)
        {
            return _userRepository.GetUserEntityByEmail(email)?.ToBllUserEntity();
        }
        
    }
}
