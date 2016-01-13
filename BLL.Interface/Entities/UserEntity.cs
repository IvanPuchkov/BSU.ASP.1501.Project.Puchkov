using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string UserPassword { get; set; }
        public string UserDisplayName { get; set; }
        public string UserEmail { get; set; }
        public bool UserBanned { get; set; }
    }
}
