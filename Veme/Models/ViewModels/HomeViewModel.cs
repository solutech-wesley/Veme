using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Veme.Models.POCO;

namespace Veme.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Offer> LatestOffers { get; set; } = new List<Offer>();

        [Required]
        public int OfferId { get; set; }
    }
}