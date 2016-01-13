using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalUserRole :IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
    }
}
