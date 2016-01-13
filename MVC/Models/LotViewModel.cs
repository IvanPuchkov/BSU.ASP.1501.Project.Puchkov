using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class LotViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CreatedByUserId { get; set; }
        public bool LotClosed { get; set; }
        public bool LotEnded { get; set; }
        [Required]
        public System.DateTime EndDate { get; set; }
        [Display(Name = "Description")]
        public string LotDescription { get; set; }
        public byte[] LotPicture { get; set; }
        public byte[] LotPicturePreview { get; set; }
        [Required]
        [Display(Name = "Starting bid")]
        public decimal MinimalBid { get; set; }
        [Display(Name = "Buy out bid")]
        public decimal? BuyOutBid { get; set; }
        public bool SearchInName { get; set; }
        public string SearchString { get; set; }
        public bool DeletePicture { get; set; }
        public UserViewModel CreatedByUser { get; set; }
        public IEnumerable<BidViewModel> LatestBids { get; set; }

    }
}