using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    public interface ILotRepository:IRepository<DalLot>
    {
        IEnumerable<DalLot> GetAllLotsCreatedByUser(int userId);
        IEnumerable<DalLot> GetLotsContainingStringInName(string s);
        IEnumerable<DalLot> GetLotsContainingStringInDescription(string s);
        void CloseLot(DalLot lot);
        void CloseAllLotsCreatedByUser(DalUser user);
    }
}
