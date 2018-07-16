using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veme.Models.POCO
{
    public class StripeCustomer
    {
        [Key]
        [MaxLength(50)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string StripeCustomerID { get; set; }

        [MaxLength(128)]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Column(TypeName="date")]
        public DateTime CreationDate { get; set; }

        public ApplicationUser User { get; set; }

    }
}