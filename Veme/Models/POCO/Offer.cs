using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veme.Models.POCO
{
    public class Offer
    {
        public int OfferId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime OfferBegins { get; set; }

        [Column(TypeName = "Date")]
        public DateTime OfferEnds { get; set; }
        public byte DiscountRate { get; set; }

        public int? TotalOffer { get; set; }

        [MaxLength(255)]
        public string OfferName { get; set; }

        [MaxLength(255)]
        public string OfferDetails { get; set; }

        [DefaultValue(0)]
        public int CouponUsed { get; set; } = 0;

        [ForeignKey("Merchant")]
        public string MerchantID { get; set; }

        public Merchant Merchant { get; set; }

        [Column(TypeName = "Date")]
        public DateTime CreationDate { get; set; }

        //Add Offer Image
        public byte[] OfferImg { get; set; }

        [Required]
        [Range(minimum:1,maximum:12)]
        public byte CouponDurationInMonths { get; set; }

        [Required]
        [Column(TypeName = "Money")]
        public decimal CouponPrice { get; set; }

    }
}