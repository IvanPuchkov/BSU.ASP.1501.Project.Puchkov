using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class BidService:IBidService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBidRepository _bidRepository;

        public BidService(IUnitOfWork uow, IBidRepository repository)
        {
            this._uow = uow;
            _bidRepository = repository;
        }

        /*void AddBet(BetEntity bet);
        IEnumerable<BetEntity> GetAllBetsByUserId(int userId);
        IEnumerable<BetEntity> GetLatestBetsByUserId(int userId); 
        void DeleteBet(BetEntity bet);
        void DeleteAllBetsByUser(int userId);*/

        public IEnumerable<BidEntity> GetAllBidsByUserId(int userId)
        {
            return _bidRepository.GetAllBidsByUserId(userId).Select(x => x.ToBllBidEntity());
        }

        /*        public IEnumerable<BidEntity> GetLatestBidsByUserId(int userId)
                {

                } */

        public BidEntity GetBidById(int id)
        {
            return _bidRepository.GetById(id).ToBllBidEntity();
        }

        public void AddBid(BidEntity bid)
        {
            try
            {
                _bidRepository.Create(bid.ToDalBid());
                _uow.Commit();
            }
            catch (DalBidTooLowException)
            {
                throw new BllBidTooLowException();
            }
        }

        public void DeleteAllBidsByUser(int userId)
        {
            _bidRepository.DeleteAllBidsMadeByUserId(userId);
            _uow.Commit();
        }

        public void DeleteBid(BidEntity bet)
        {
            _bidRepository.Delete(bet.ToDalBid());
            _uow.Commit();
        }

        public IEnumerable<BidEntity> GetAllBids()
        {
            return _bidRepository.GetAll().Select(bid => bid.ToBllBidEntity());
        }

        public IEnumerable<BidEntity> GetLatestBidsForLot(int lotId)
        {
            return _bidRepository.GetLatestBidsForLot(lotId).Select(x => x.ToBllBidEntity());
        }

    }
}
