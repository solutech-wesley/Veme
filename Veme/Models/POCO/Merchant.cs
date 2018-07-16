using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veme.Models.POCO
{
    public class Merchant
    {
        [Key]
        [MaxLength(128)]
        [ForeignKey("User")]
        public string MerchantID { get; set; }

        public string CompanyName { get; set; }

        public string CompanyWebsite { get; set; }

        public string CompanyDescriptiton { get; set; }

        //User Navigation property
        public ApplicationUser User { get; set; }

        //Navigation proper for many-to-many relationship
        public virtual ICollection<MerchantAddress> Addresses { get; set; }

        //Create Many-to-many relationship between merchant and Offer
        public virtual ICollection<Category> Categories { get; set; }
    }

    public class MerchantAddress
    {

        public int MerchantAddressId { get; set; }

        [MaxLength(255)]
        public string StreetAddress1 { get; set; }

        [MaxLength(255)]
        public string StreetAddress2 { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string Parish { get; set; }

        public string Country { get; set; }

        
        public virtual ICollection<Merchant> Merchants { get; set; }

    }

    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        //Create Many-to-many relationship between merchant and Offer
        public virtual ICollection<Merchant> Merchants { get; set; }
    }
}