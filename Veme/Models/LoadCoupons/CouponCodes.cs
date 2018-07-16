using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Veme.Models
{
    public class CouponCodes
    {
        static ApplicationDbContext _context = new ApplicationDbContext();

        public static string base36Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string ToBase36(int x, int digits)
        {
            char[] result = new char[digits];
            for (int i = digits - 1; i >= 0; --i)
            {
                result[i] = base36Characters[x % 36];
                x /= 36;
            }
            return new string(result);
        }

        //
        public static IEnumerable<string> Base36Counter(int startPoint, int endPoint)
        {
            for (int n = startPoint; n < (startPoint + endPoint); ++n)
            {
                yield return ToBase36(n, digits: 9);
            }
        }


        public static async Task Run(int startPoint = 372, int endPoint = 100)
        {
            foreach (string s in Base36Counter(startPoint, endPoint))
            {
                //Console.WriteLine(s);
                _context = new ApplicationDbContext();
                _context.CouponRepositories.Add(new POCO.CouponRepository { CouponCode = s.Insert(3,"-").Insert(7,"-") });
                await _context.SaveChangesAsync();
            }
        }

        #region
        private static Random random = new Random();

        public static string CouponFormat(int CouponLength, string coupon)
        {
            if (CouponLength == 6)
                return coupon.Insert(3, "-");

            return coupon.Insert(3, "-").Insert(7, "-");
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string coupon = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray()); ;
            return CouponFormat(length, coupon);//coupon.Insert(3, "-").Insert(7, "-"); // return 9-digit-coupon-code

        }
        #endregion
    }
}