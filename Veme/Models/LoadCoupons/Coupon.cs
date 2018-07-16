using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Veme.Models
{
    public class Coupon
    {
        public int ID { get; set; }
        [DisplayName("Offer Ends")]
        public DateTime OfferEnds { get; set; }

    }
}