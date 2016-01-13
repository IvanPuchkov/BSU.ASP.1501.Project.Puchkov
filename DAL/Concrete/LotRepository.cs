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
    public class LotRepository:ILotRepository
    {
        

        private readonly DbContext _context;

        public LotRepository(DbContext uow)
        {
            _context = uow;
        }

        public IEnumerable<DalLot> GetAll()
        {
            return _context.Set<Lot>().Select(lot => new DalLot
            {
                Id = lot.Id,
                BuyOutBet = lot.BuyOutBet,
                CreatedByUserId = lot.CreatedByUserId,
                EndDate = lot.EndDate,
                LotClosed = lot.LotClosed,
                LotEnded = lot.LotEnded,
                LotDescription = lot.LotDescription,
                Name = lot.Name,
                LotPicture = lot.LotPicture,
                LotPicturePreview = lot.LotPicturePreview,
                MinimalBet = lot.MinimalBet,
                CreatedByUser = new DalUser
                {
                    Id = lot.UserLogin.Id,
                    UserDisplayName = lot.UserLogin.UserDisplayName,
                    UserBanned = lot.UserLogin.UserBanned,
                    UserEmail = lot.UserLogin.UserEmail,
                    UserPassword = lot.UserLogin.UserPassword
                }
            });
        }

        public DalLot GetById(int key)
        {
            var ormLot = _context.Set<Lot>().FirstOrDefault(lot => lot.Id == key);

            return ormLot.ToDalLot();
        }

        public void Create(DalLot e)
        {

            _context.Set<Lot>().Add(e.ToOrmLot());
        }

        public void Delete(DalLot e)
        {
            var lot = e.ToOrmLot();
            lot = _context.Set<Lot>().Single(u => u.Id == lot.Id);
            _context.Set<Lot>().Remove(lot);
        }

        public void Update(DalLot e)
        {
            var lot = _context.Set<Lot>().Single(u => u.Id == e.Id);
            lot.Id = e.Id;
            lot.BuyOutBet = e.BuyOutBet;
            lot.CreatedByUserId = e.CreatedByUserId;
            lot.EndDate = e.EndDate;
            lot.LotClosed = e.LotClosed;
            lot.LotEnded = e.LotEnded;
            lot.LotDescription = e.LotDescription;
            lot.Name = e.Name;
            lot.LotPicture = e.LotPicture;
            lot.LotPicturePreview = e.LotPicturePreview;
            lot.MinimalBet = e.MinimalBet;
            _context.Entry(lot).State = EntityState.Modified;
        }

        public IEnumerable<DalLot> GetAllLotsCreatedByUser(int userId)
        {
            return  _context.Set<Lot>().Where(x => x.CreatedByUserId == userId).Select(x => x.ToDalLot());
        }

        public IEnumerable<DalLot> GetLotsContainingStringInName(string s)
        {
            return _context.Set<Lot>().Where(a => a.Name.Contains(s)).ToEnumerable().Select(x => x.ToDalLot());
        }

        public IEnumerable<DalLot> GetLotsContainingStringInDescription(string s)
        {
            return _context.Set<Lot>().Where(a => a.LotDescription.Contains(s)).ToEnumerable().Select(x => x.ToDalLot());
        }

        public void CloseLot(DalLot lot)
        {
            var ormlot = _context.Set<Lot>().Single(u => u.Id == lot.Id);
            ormlot.LotClosed = true;
            _context.Entry(ormlot).State = EntityState.Modified;
        }

        public void CloseAllLotsCreatedByUser(DalUser user)
        {
            var ormLots =
                _context.Set<Lot>()
                    .Where(x => x.CreatedByUserId == user.Id)
                    .Where(x => x.LotClosed == false)
                    .Where(x => x.LotEnded == false);
            foreach (var lot in ormLots)
            {
                lot.LotClosed = true;
                _context.Entry(lot).State=EntityState.Modified;
            }
        }
    }
}
