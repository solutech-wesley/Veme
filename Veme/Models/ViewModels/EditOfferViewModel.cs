using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Veme.Models.POCO;

namespace Veme.Models.ViewModels
{
    public class EditOfferViewModel
    {
        [DisplayName("Merchants")]
        public IEnumerable<Merchant> Merchants { get; set; }

        //[Required(ErrorMessage = "Select Merchant")]
        //public int MerchantID { get; set; }
        [Required(ErrorMessage = "Select Merchant")]
        public string MerchantID { get; set; }

        [DisplayName("Offers")]
        [Required(ErrorMessage = "Offer needed")]
        public int Offer_Id { get; set; }

        public IEnumerable<Offer> Offers { get; set; }


    }
}