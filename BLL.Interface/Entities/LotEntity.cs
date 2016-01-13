using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class LotEntity
    {
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
        public decimal? BuyOutBet { get; set; }
        public UserEntity CreatedByUser { get; set; }
    }
}
