using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalBid:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LotId { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Placed { get; set; }
        public DalLot Lot { get; set; }
        public DalUser User { get; set; }
    }
}
