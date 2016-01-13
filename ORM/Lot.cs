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
    
    public partial class Lot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lot()
        {
            this.Bid = new HashSet<Bid>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreatedByUserId { get; set; }
        public bool LotClosed { get; set; }
        public bool LotEnded { get; set; }
        public System.DateTime EndDate { get; set; }
        public string LotDescription { get; set; }
        public byte[] LotPicture { get; set; }
        public byte[] LotPicturePreview { get; set; }
        public decimal MinimalBet { get; set; }
        public Nullable<decimal> BuyOutBet { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bid { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}
