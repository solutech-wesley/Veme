using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Veme.Models.POCO
{
    public class Customer
    {
        
        [Key]
        [MaxLength(128)]
        [ForeignKey("User")]
        public string CustomerId { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DOB { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}