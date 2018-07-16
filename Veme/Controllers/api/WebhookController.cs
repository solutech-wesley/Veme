using Newtonsoft.Json.Linq;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using Veme.Models;

namespace Veme.Controllers.api
{
    public class WebhookController : ApiController
    {
        private ApplicationDbContext _context;

        public WebhookController()
        {
            //_context = new Lookup_Entities();
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Index()
        {
            StripeConfiguration.SetApiKey(WebConfigurationManager.AppSettings["StripeSecretKey"]);

            var json = new StreamReader(HttpContext.Current.Request.InputStream).ReadToEnd();
            var stripeEvent = StripeEventUtility.ParseEvent(json);
            var customerService = new StripeCustomerService();
            string stripeCustomerID = stripeEvent.Data.Object.customer;//as string;
            var stripeCustomer = new Stripe.StripeCustomer();

            //Test 
            var eventObjectInfo = JObject.Parse(json);
            var evtDataObject = eventObjectInfo.SelectToken("data.object");
            switch (stripeEvent.Type)
            {
                case StripeEvents.PayoutPaid:
                    //var customer = Mapper<Stripe.StripeCustomer>.MapFromJson(eventObjectInfo.ToString());
                    return Json(new { success = true, message = "Hello world number" });

                case StripeEvents.InvoicePaymentFailed:
                    return Json(new { id = stripeEvent.Id, message = "User Subcription Paused" });

                case StripeEvents.ChargeSucceeded:
                    string dytoString = Convert.ToString(stripeCustomerID);
                    var obj = _context.StripeCustomers.FirstOrDefault(c => c.StripeCustomerID == dytoString);
                    if(obj != null)
                    {

                    }

                    return Json(new { type = stripeEvent.Type, stripeEvent.Type });

                case StripeEvents.InvoiceCreated:
                    //string custID = stripeEvent.Data.Object.customer;
                    //var getCustomer = customerService.Get(custID);
                    var evt = Mapper<StripeEvent>.MapFromJson(eventObjectInfo.ToString());
                    return Json(new { evt.Id, evt.Data.Object.customer });

                case StripeEvents.InvoicePaymentSucceeded:
                        return Json(new { message = "Invoice Payment Successful" });
                default:
                    return Json(new { id = stripeEvent.Id });
            }
        }

        public string CentToDollars(string cent)
        {
            var toInt = Convert.ToInt32(cent);
            if (toInt == 0)
                return "0";
            return Convert.ToString((toInt / 100).ToString("C"));
        }

    }
}
