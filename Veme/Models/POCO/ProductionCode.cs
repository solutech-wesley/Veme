using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veme.Models.POCO
{
    public class ProductionCode
    {
        public int ProductionCodeID { get; set; }

        [StringLength(12)]
        public string CouponCode { get; set; }

        [DefaultValue(false)]
        public bool IsUsed { get; set; }

        [DefaultValue(false)]
        public bool IsActive { get; set; }

        [ForeignKey("Offers")]
        public int? OfferId { get; set; }

        public Offer Offers { get; set; }

    }

    public class CouponRepository
    {
        public int CouponRepositoryID { get; set; }

        [StringLength(12)]
        [Index(IsUnique = true)]
        public string CouponCode { get; set; }

        [DefaultValue("false")]
        public bool InProduction { get; set; }

    }

}