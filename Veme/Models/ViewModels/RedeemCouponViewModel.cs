using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Veme.Models.POCO;

namespace Veme.Models.ViewModels
{
    public class AdminCouponViewModel
    {
        [StringLength(12)]
        [RegularExpression(@"^\w{3}-\w{3}-\w{3}",ErrorMessage ="Invalid Format")]
        [DisplayName("Coupon Code")]
        public string CouponCode { get; set; }

        public int MerchantId { get; set; }
        public IEnumerable<Merchant> Merchants { get; set; }
    }

    public class MerchantRedeemViewModel
    {
        [StringLength(12)]
        [RegularExpression(@"^\w{3}-\w{3}-\w{3}", ErrorMessage = "Invalid Format")]
        [DisplayName("Coupon Code")]
        public string CouponCode { get; set; }

        public string MerchantId { get; set; }
    }

}