using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IBidService
    {
        void AddBid(BidEntity bid);
        BidEntity GetBidById(int id);
        IEnumerable<BidEntity> GetAllBids();
        IEnumerable<BidEntity> GetAllBidsByUserId(int userId);
        IEnumerable<BidEntity> GetLatestBidsForLot(int lotId); 
        void DeleteBid(BidEntity bid);
        void DeleteAllBidsByUser(int userId);
    }
}
