using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BllBidTooLowException:Exception
    {
        public BllBidTooLowException() : base() { }
        public BllBidTooLowException(string message) : base(message) { }
        public BllBidTooLowException(string message, System.Exception inner) : base(message, inner) { }
    }
}
