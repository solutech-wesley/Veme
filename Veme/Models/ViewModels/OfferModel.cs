using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Web;
using Veme.Models.POCO;

namespace Veme.Models
{
    public class OfferModel
    {
        public int? OFFERID { get; set; } = null;
        [DisplayName("Discount")]
        [Required]
        public byte Discount { get; set; }

        [DisplayName("Offer Name")]
        public string OfferName { get; set; }

        [DisplayName("Offer Conditions")]
        public string OfferDescription { get; set; }
         
        [DisplayName("Merchant")]
        [Required]
        public string OffererId { get; set; }

        public string MerchantName { get; set; }

        [DisplayName("Offer Starts")]
        public DateTime? OfferStarts { get; set; }

        [DisplayName("Offer Expires")]
        public DateTime? OfferEnds { get; set; }

        [DisplayName("Quantity")]
        public int TotalOffers { get; set; }

        public string FormatDate { get; set; }

        public IEnumerable<Merchant> Merchants { get; set; }
        //public IEnumerable<string> Merchants = new List<string>() { "KFC", "Burger King" };
    }

    public class MerchantCreateOfferViewModel
    {
        public int? OfferId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy", ApplyFormatInEditMode = true)]
        public DateTime? OfferBegins { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyy",ApplyFormatInEditMode =true)]
        public DateTime? OfferEnds { get; set; }

        public byte? DiscountRate { get; set; }

        public int? TotalOffer { get; set; }

        [MaxLength(255)]
        public string OfferName { get; set; }

        [MaxLength(255)]
        public string OfferDetails { get; set; }

        public int CouponUsed { get; set; }

        public string MerchantID { get; set; }

        public Merchant Merchant { get; set; }

        public DateTime CreationDate { get; set; }

        ////Add Offer Image
        //public byte[] OfferImg { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name ="Offer Display Image")]
        public HttpPostedFileBase OfferImg { get; set; }

        [Required]
        [Range(minimum: 1, maximum: 12)]
        public byte? CouponDurationInMonths { get; set; }

        [Required]
        public decimal? CouponPrice { get; set; }

        public byte[] ConvertImgToByteArray()
        {
            //1. Get stream from Image file and return byte Array
            //var stream = uploadedImg.InputStream;
            var stream = OfferImg.InputStream;
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            var ms = new MemoryStream(buffer);
            return ms.ToArray();
            #region Convert StreamTo Img
            //convert stream into image
            //Image myImage = Image.FromStream(new MemoryStream(buffer));
            //myImage.Save(@"c:\myimage.jpg");
            //using (var ms = new MemoryStream())
            //{
            //    OfferImg.Save(ms, OfferImg.RawFormat);
            //    return ms.ToArray();
            //}
            #endregion
        }
    }

    public class CouponDetailsViewModel
    {
        public Offer Offer { get; set; }

    }
}