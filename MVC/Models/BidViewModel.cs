using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class BidViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LotId { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Placed { get; set; }
        public LotViewModel Lot { get; set; }
        public UserViewModel User { get; set; }
    }
}