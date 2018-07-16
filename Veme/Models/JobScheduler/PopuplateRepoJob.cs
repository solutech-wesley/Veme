using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Veme.Models
{
    public class PopuplateRepoJob : IJob
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private int  GenerateCode = 5000;
        public async void Execute(IJobExecutionContext context)
        {
            //throw new NotImplementedException();
            //Get All Coupons
            var getAllCoupons = _context.CouponRepositories.Count();

            //Gets coupons in production
            var inProduction = _context.CouponRepositories.Where(x => x.InProduction == true).Count();

            if (getAllCoupons == 0)
                await CouponCodes.Run(getAllCoupons, GenerateCode);

            if (await DoIGenerateAsync(inProduction: inProduction, totalCouponsInRepo: getAllCoupons))
                await CouponCodes.Run(getAllCoupons, GenerateCode);
        }

        //Calculate percentage of coupon used
        //If used coupon percentage greater than or equal to 80% return true
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inProduction">The amount of coupons that are sent to production for use.</param>
        /// <param name="totalCouponsInRepo">The total amount of coupons in the repo.</param>
        /// <returns>Return a true or false value</returns>
        async Task<bool> DoIGenerateAsync(int inProduction, int totalCouponsInRepo)
        {
            var percentage = (inProduction / totalCouponsInRepo) * 100;

            if (percentage >= 80)
                return true;

            return false;
        }

    }
}