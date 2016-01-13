using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface ILotService
    {
        LotEntity GetLotEntity(int id);
        IEnumerable<LotEntity> GetAllLotsCreatedByUserId(int userId);
        IEnumerable<LotEntity> GetAllLots();
        IEnumerable<LotEntity> GetLotsContainingStringInName(string s);
        IEnumerable<LotEntity> GetLotsContainingStringInDescription(string s);
        void CreateLot(LotEntity lot);
        void DeleteLot(LotEntity lot);
        void CloseLot(LotEntity lot);
        void UpdateLot(LotEntity lot);
        void CloseAllActiveLotsCreatedByUser(UserEntity user);
    }
}
