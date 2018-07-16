using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Veme.Models.POCO;

namespace Veme.Models.ViewModels
{
    public class PurchaseCouponViewModel
    {
        [DisplayName("Merchants")]
        public IEnumerable<Merchant> Merchants { get; set; }

        [Required(ErrorMessage ="Select Merchant")]
        public int OfferId { get; set; }

        [DisplayName("Offers")]
        [Required(ErrorMessage ="Offer needed")]
        public int Offer_Id { get; set; }

        public IEnumerable<Offer> Offer { get; set; }
    }
}