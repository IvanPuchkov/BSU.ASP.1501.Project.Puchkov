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
    public class RoleService:IRoleService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this._uow = uow;
            _roleRepository = repository;
        }

        public RoleEntity GetRoleByName(string name)
        {
            return _roleRepository.GetRoleByName(name).ToBllRoleEntity();
        }

        public RoleEntity GetRoleById(int id)
        {
            return _roleRepository.GetById(id).ToBllRoleEntity();
        }

        public void AddRoleEntity(RoleEntity roleEntity)
        {
            _roleRepository.Create(roleEntity.ToDalRole());
            _uow.Commit();
        }

        public void DeleteRoleEntity(RoleEntity roleEntity)
        {
            _roleRepository.Delete(roleEntity.ToDalRole());
            _uow.Commit();
        }

        public void UpdateRoleEntity(RoleEntity roleEntity)
        {
            _roleRepository.Update(roleEntity.ToDalRole());
            _uow.Commit();
        }

        public IEnumerable<RoleEntity> GetAllRoles()
        {
            return _roleRepository.GetAll().Select(x => x.ToBllRoleEntity());
        }

    }
}
