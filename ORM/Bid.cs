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
    
    public partial class Bid
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LotId { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Placed { get; set; }
    
        public virtual Lot Lot { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}
