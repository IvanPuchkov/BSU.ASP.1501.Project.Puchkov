﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class UserRoleEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
    }
}
