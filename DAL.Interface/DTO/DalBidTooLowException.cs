using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalBidTooLowException:Exception
    {
        public DalBidTooLowException() : base() { }
        public DalBidTooLowException(string message) : base(message) { }
        public DalBidTooLowException(string message, System.Exception inner) : base(message, inner) { }
    }
}
