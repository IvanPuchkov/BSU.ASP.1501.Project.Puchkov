using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BidEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LotId { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Placed { get; set; }
        public LotEntity Lot { get; set; }
        public UserEntity User { get; set; }
    }
}
