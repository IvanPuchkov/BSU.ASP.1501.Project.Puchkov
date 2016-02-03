using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Mappers;
using Ninject.Infrastructure.Language;
using ORM;

namespace DAL.Concrete
{
    public class BidRepository:IBidRepository
    {
        private readonly DbContext _context;

        public BidRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalBid> GetAll()
        {
            return _context.Set<Bid>().ToEnumerable().Select(bid => bid.ToDalBid());
        }

        public DalBid GetById(int key)
        {
            var ormBid = _context.Set<Bid>().FirstOrDefault(bid => bid.Id == key);

            return ormBid?.ToDalBid();
        }

        public void DeleteAllBidsMadeByUserId(int userId)
        {
            _context.Set<Bid>().RemoveRange(_context.Set<Bid>().Where(x => x.UserLogin.Id == userId).Where(x=>((!x.Lot.LotClosed)&&(!x.Lot.LotEnded))));
        }

        public IEnumerable<DalBid> GetAllBidsByUserId(int userId)
        {
            return _context.Set<Bid>().Where(x => x.UserId == userId).ToEnumerable().Select(x => x.ToDalBid());
        }

        public void Create(DalBid e)
        {
            var x = e.ToOrmBid();
            
            _context.Set<Bid>().Add(x);
            _context.Entry(x).State=EntityState.Added;
        }

        public void Delete(DalBid e)
        {
            var bid = e.ToOrmBid();
            bid = _context.Set<Bid>().Single(u => u.Id == bid.Id);
            _context.Set<Bid>().Remove(bid);
        }

        public void Update(DalBid e)
        {
            var bid = _context.Set<Bid>().Single(u => u.Id == e.Id);
            bid.Amount=e.Amount;
            bid.LotId = e.LotId;
            bid.Placed = e.Placed;
            bid.UserId = e.UserId;
            _context.Entry(bid).State = EntityState.Modified;
        }

        public IEnumerable<DalBid> GetLatestBidsForLot(int lotId)
        {
            var a =
                _context.Set<Bid>()
                    .Include(x => x.UserLogin)
                    .Where(x => x.LotId == lotId)
                    .OrderByDescending(x => x.Amount)
                    .ToEnumerable()
                    .Select(x => x.ToDalBid());
            return a;
        }
    }
}
