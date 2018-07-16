using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Veme.Models.POCO;

namespace Veme.Models
{
    public static class LoadProductionCoupon
    {
        private static ApplicationDbContext _context = new ApplicationDbContext();

        //1.Gets the last point where Codes were taken from the repo
        private static int GetCheckPoint()
        {
            return _context.CheckPoints
                           .OrderByDescending(c => c.CheckPointID)
                           .Select(c => c.LastPoint)
                           .FirstOrDefault();
        }

        public static async Task LoadCoupons(int allotCoupons = 100)
        {
            var lastPoint = GetCheckPoint();

            //get all codes not in production
            var availCoupons = _context.CouponRepositories
                                       .Where(c => c.InProduction == false)
                                       .Count();
            
            //get all code in the repository
            var totalRepo = _context.CouponRepositories.Count();

            if (allotCoupons >= availCoupons)
                await CouponCodes.Run(totalRepo, allotCoupons);

            var getCodes = _context.CouponRepositories
                                   .Where(c => c.CouponRepositoryID >= (lastPoint + 1) && c.CouponRepositoryID <= (lastPoint + allotCoupons))
                                   .ToList();

            //Load Repo Code to Production
            //Update Repo Code Status In repository
            foreach(var code in getCodes)
            {
                _context = new ApplicationDbContext();
                var productionCode = new ProductionCode
                {
                    CouponCode = code.CouponCode
                };
                _context.ProductionCodes.Add(productionCode);
                var repoCode = _context.CouponRepositories.FirstOrDefault(c => c.CouponRepositoryID == code.CouponRepositoryID);
                repoCode.InProduction = true;
                await _context.SaveChangesAsync();
            }

            //Update Check point
            var lastValue = getCodes[getCodes.Count - 1];
            _context.CheckPoints.Add(new CheckPoint { LastPoint = lastValue.CouponRepositoryID });
            await _context.SaveChangesAsync();

        }
    }
}