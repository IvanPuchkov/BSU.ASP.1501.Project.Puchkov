using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface IBidRepository:IRepository<DalBid>
    {
        void DeleteAllBidsMadeByUserId(int userId);
        IEnumerable<DalBid> GetAllBidsByUserId(int userId);
        IEnumerable<DalBid> GetLatestBidsForLot(int lotId);
        //IEnumerable<DalBid> GetLatestBidsByUserId(int userId);
    }
}
