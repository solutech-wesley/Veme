using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Veme.Models.POCO;
using System.Data.Entity;

namespace Veme.Models
{
    public static  class Payment
    {
        static ApplicationDbContext _context = new ApplicationDbContext();

        //Collect single charge for coupon
        public static bool Charge(string stripeToken, Offer offer)
        {
            StripeConfiguration.SetApiKey(WebConfigurationManager.AppSettings["StripeSecretKey"]);

            var token = stripeToken;
            var loginUser = _context.Merchants.Include(c => c.User).FirstOrDefault(c => c.MerchantID == offer.MerchantID);
            //Create stripe customerID
            var customerId = CreateStripeCustomer(loginUser.User.Id, token);

            var option = new StripeChargeCreateOptions
            {
                Amount = Convert.ToInt32(offer.CouponPrice * 100),
                Currency = "usd",
                Description = "Veme",
                ReceiptEmail = loginUser.User.Email, // returns the email address
                //SourceTokenOrExistingSourceId = token,
                CustomerId = customerId

            };
            var service = new StripeChargeService();
            StripeCharge charge = service.Create(option);
            //if (charge.Paid)
            //return Json(new { charge.Paid});
            return charge.Paid;
        }

        private static string CreateStripeCustomer(string userId, string stripeToken)
        {
            var customerService = new StripeCustomerService();
            var getStripeCustomer = _context.StripeCustomers.FirstOrDefault(c => c.UserId == userId);

            if (getStripeCustomer != null)
                return getStripeCustomer.StripeCustomerID;

            var getUser = _context.Users.FirstOrDefault(c => c.Id == userId);

            var customerOptions = new StripeCustomerCreateOptions()
            {
                Description = "Veme Customer for joseph.garcia@example.com",
                SourceToken = stripeToken,
                Email = getUser.Email
            };

            Stripe.StripeCustomer customer = customerService.Create(customerOptions);

            _context.StripeCustomers.Add(new POCO.StripeCustomer
            {
                CreationDate = DateTime.Now.Date,
                StripeCustomerID = customer.Id,
                UserId = userId
            });
            _context.SaveChanges();
            return customer.Id;
        }
    }
}