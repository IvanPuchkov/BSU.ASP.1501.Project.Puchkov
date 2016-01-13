using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class UserRoleService:IUserRoleSevice
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRoleRepository _roleRepository;

        public UserRoleService(IUnitOfWork uow, IUserRoleRepository repository)
        {
            this._uow = uow;
            _roleRepository = repository;
        }

        public UserRoleEntity GetUserRoleEntityById(int id)
        {
            return _roleRepository.GetById(id).ToBllUserRoleEntity();
        }

        public void AddUserRoleEntity(UserRoleEntity userRoleEntity)
        {
            _roleRepository.Create(userRoleEntity.ToDalUserRole());
            _uow.Commit();
        }

        public void DeleteUserRoleEntity(UserRoleEntity userRoleEntity)
        {
            _roleRepository.Delete(userRoleEntity.ToDalUserRole());
            _uow.Commit();
        }

        public IEnumerable<UserRoleEntity> GetAllUserRoleEntities()
        {
            return _roleRepository.GetAll().Select(x => x.ToBllUserRoleEntity());
        }

        public IEnumerable<RoleEntity> GetUserRoles(int userId)
        {
            return _roleRepository.GetUserRoles(userId).Select(userRole => userRole.ToBllRoleEntity());
        }

        public IEnumerable<UserEntity> GetUsersWithRoleId(int roleId)
        {
            return _roleRepository.GetUsersWithRoleId(roleId).Select(user => user.ToBllUserEntity());
        }

    }
}
