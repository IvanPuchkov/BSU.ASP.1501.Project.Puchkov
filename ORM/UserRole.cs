//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ORM
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRole
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}
