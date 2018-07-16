using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using Veme.Models;
using Veme.Models.POCO;
using System.Data.Entity;

namespace LookUp.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Error()
        //{
        //    return View();
        //}

        //public static ActionResult Charge(string stripeEmail, string stripeToken, string SubscriptionId, int UserID)
        //{
        //    StripeConfiguration.SetApiKey(WebConfigurationManager.AppSettings["StripeSecretKey"]);

        //    CreateStripePlan(SubscriptionId);
        //    var customerID = CreateStripeCustomer(stripeEmail, stripeToken, UserID);
        //    SubscribeToStripePlan(SubscriptionId, customerID);

        //    AssignPlan(Convert.ToInt32(SubscriptionId));
        //    RefreshCustomerAPICalls(UserID);
        //    return RedirectToAction("Index", "Subscription");
        //}


        //private void AssignPlan(int planID)
        //{
        //    var user = (UserViewModel)Session["user"];  
        //    //_context.AssignPlanById(user.UserID,planID);
        //}

        //private void ChargeCustomer(string stripeToken)
        //{
        //    var chargeDetails = new StripeChargeCreateOptions();
        //    chargeDetails.Amount = 500;
        //    chargeDetails.Description = "Pay for plan";
        //    chargeDetails.Currency = "usd";
        //    chargeDetails.Capture = true;
        //    chargeDetails.SourceTokenOrExistingSourceId = stripeToken;
        //    var chargeService = new StripeChargeService();
        //    StripeCharge stripeCharge = null;

        //    stripeCharge = chargeService.Create(chargeDetails);

        //    if (stripeCharge == null)
        //        return;
        //        //return HttpNotFound("Payment was not accepted");
        //}

        //private void CreateStripePlan(string planID)
        //{
        //    var planService = new StripePlanService();
        //    var planNumber = Convert.ToInt32(planID);
        //    var getPlan = _context.Subscription_Plan.FirstOrDefault(c => c.SubscriptionId == planNumber);

        //    if (getPlan == null)
        //        return;

        //    try
        //    {
        //        StripePlan checkPlan = planService.Get(planID);

        //        if (checkPlan != null)
        //            return;

        //        var options = new StripePlanCreateOptions
        //        {
        //            Currency = "usd",
        //            Interval = "month",
        //            Name = getPlan.Subscription_Plan1,
        //            Amount = (int)getPlan.MonthyFee * 100,
        //            Id = getPlan.SubscriptionId.ToString(),
        //            IntervalCount = 1,
        //            StatementDescriptor = "HLR Lookup"
        //        };
        //        var service = new StripePlanService();
        //        StripePlan plan = service.Create(options);
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Trim() == string.Format("No such plan: {0}",planID).Trim())
        //        {

        //            if (getPlan == null)
        //                return;

        //            var options = new StripePlanCreateOptions
        //            {
        //                Currency = "usd",
        //                Interval = "month",
        //                Name = getPlan.Subscription_Plan1,
        //                Amount = (int)getPlan.MonthyFee * 100,
        //                Id = getPlan.SubscriptionId.ToString(),
        //                IntervalCount = 1,
        //                StatementDescriptor = "HLR Lookup"
        //            };
        //            var service = new StripePlanService();
        //            StripePlan plan = service.Create(options);
        //            return;
        //        }
        //    }
        //}

        //public static string CreateStripeCustomer(string customerEmail,string stripeToken, int customerID)
        //{
        //    var customerService = new StripeCustomerService();
        //    var getStripeCustomer = _context.StripeCustomers.FirstOrDefault(c => c.UserID == customerID);
        //    if (getStripeCustomer != null)
        //        return getStripeCustomer.StripeID;
        //    //StripeCustomer stripeCustomer = customerService.Get(customerId);
        //    //if (stripeCustomer != null)
        //    //    return;

        //    //Set the plan ID below
        //    var options = new StripeCustomerCreateOptions
        //    {
        //       Email = customerEmail,
        //       SourceToken = stripeToken              
        //    };

        //    var service = new StripeCustomerService();
        //    Stripe.StripeCustomer customer = service.Create(options);
        //    _context.CreateStripeCustomer(customerID,customer.Email,customer.Id);            
        //    return customer.Id;
        //}

        //private void RefreshCustomerAPICalls(int customerID)
        //{
        //    var get = _context.Users.SingleOrDefault(c => c.UserID == customerID);
        //    get.Calls_Made = 0;
        //    _context.SaveChanges();
        //    return;
        //}

        //public static void SubscribeToStripePlan(string planID, string customerID)
        //{
        //    var planNumber = Convert.ToInt32(planID);
        //    var getPlan = _context.Subscription_Plan.FirstOrDefault(c => c.SubscriptionId == planNumber);

        //    if (getPlan == null)
        //        return;
        //    //check and remove subscription ID
        //    CheckAndDeleteCustomerPlan(customerID);

        //    var items = new List<StripeSubscriptionItemOption>
        //    {
        //        new StripeSubscriptionItemOption{PlanId = getPlan.SubscriptionId.ToString() }
        //    };

        //    var options = new StripeSubscriptionCreateOptions
        //    {
        //        Items = items
        //    };

        //    var service = new StripeSubscriptionService();
        //    StripeSubscription subscription = service.Create(customerID, options);
        //}

        //public static void CheckAndDeleteCustomerPlan(string customerID)
        //{
        //    //check if customer has plan
        //    var customerService = new StripeCustomerService();
        //    var stripeCustomer = customerService.Get(customerID);
        //    var subscriptionService = new StripeSubscriptionService();
        //    //StripeSubscription subscription = null;

        //    if (stripeCustomer.Subscriptions.Data != null)
        //    {
        //        foreach (var plan in stripeCustomer.Subscriptions.Data)
        //            subscriptionService.Cancel(plan.Id);
        //    }
        //}

    }
}