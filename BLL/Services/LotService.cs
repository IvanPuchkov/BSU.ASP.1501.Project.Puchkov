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
    public class LotService : ILotService
    {
        


        private readonly IUnitOfWork _uow;
        private readonly ILotRepository _lotRepository;

        public LotService(IUnitOfWork uow, ILotRepository repository)
        {
            this._uow = uow;
            _lotRepository = repository;
        }

        public LotEntity GetLotEntity(int id)
        {
            return _lotRepository.GetById(id).ToBllLotEntity();
        }

        public void CreateLot(LotEntity lot)
        {
            _lotRepository.Create(lot.ToDalLot());
            _uow.Commit();
        }

        public IEnumerable<LotEntity> GetAllLots()
        {
            return _lotRepository.GetAll().Select(lot => lot.ToBllLotEntity());
        }

        public void DeleteLot(LotEntity lot)
        {
            _lotRepository.Delete(lot.ToDalLot());
            _uow.Commit();
        }

        public IEnumerable<LotEntity> GetAllLotsCreatedByUserId(int userId)
        {
            return _lotRepository.GetAllLotsCreatedByUser(userId).Select(a => a.ToBllLotEntity());

        }

        public IEnumerable<LotEntity> GetLotsContainingStringInName(string s)
        {
            return _lotRepository.GetLotsContainingStringInName(s).Select(a => a.ToBllLotEntity());
        }

        public IEnumerable<LotEntity> GetLotsContainingStringInDescription(string s)
        {
            return _lotRepository.GetLotsContainingStringInDescription(s).Select(a => a.ToBllLotEntity());
        }

        public void UpdateLot(LotEntity lot)
        {
            _lotRepository.Update(lot.ToDalLot());
            _uow.Commit();
        }

        public void CloseLot(LotEntity lot)
        {
            _lotRepository.CloseLot(lot.ToDalLot());
            _uow.Commit();
        }

        public void CloseAllActiveLotsCreatedByUser(UserEntity user)
        {
            _lotRepository.CloseAllLotsCreatedByUser(user.ToDalUser());
            _uow.Commit();
        }
    }
}
