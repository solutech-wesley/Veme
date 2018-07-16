using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veme.Models;
using Veme.Models.ViewModels;
using System.Data.Entity;
using System.Net.Mail;
using System.Web.Configuration;
using System.Net;
using System.Web.Security;

namespace Veme.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.PublicKey = WebConfigurationManager.AppSettings["StripePublicKeyTest"];
            var viewModel = new HomeViewModel()
            {
                LatestOffers = _context.Offers.Include(c=> c.Merchant).OrderByDescending(c => c.CreationDate).Take(8).ToList()
            };
            return View(viewModel);
        }

        public ActionResult CouponDetails(int OfferId)
        {
            ViewBag.PublicKey = WebConfigurationManager.AppSettings["StripePublicKeyTest"];

            var getOffer = _context.Offers.Include(c => c.Merchant).FirstOrDefault(c => c.OfferId == OfferId);
            if (getOffer == null)
                return RedirectToAction("Index");

            var details = new CouponDetailsViewModel
            {
                Offer = getOffer
            };
            return View(details);
        }

        public ActionResult GetCoupon(int OfferId, string stripeToken)
        {
            //if (!ModelState.IsValid)
            //1.Return View if something is wrong
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            //2.Get the Offer Select
            var getOffer = _context.Offers.Include(c => c.Merchant).FirstOrDefault(c => c.OfferId == OfferId);

            //2. Charge the customer
            var charge = Payment.Charge(stripeToken, getOffer);

            //Check if the money was paid
            //Return the user 
            if (!charge)
                return RedirectToAction("Index");

            //3.Gets a random code from Production Table to assign to the Coupon
            var getRandomProductionCode = _context.ProductionCodes.Where(c => c.IsActive == false && c.IsUsed == false && c.OfferId == null).OrderBy(c => Guid.NewGuid()).FirstOrDefault();

            //4.The Production Code that was receive
            //will be assigned to the Offer and label as Active.
            if (getRandomProductionCode != null)
            {
                getRandomProductionCode.OfferId = OfferId;
                getRandomProductionCode.IsActive = true;
                _context.SaveChanges();
            }

            var details = new GenCouponViewModel()
            {
                OfferDetails = getOffer,
                CouponCode = getRandomProductionCode.CouponCode
            };
            //return RedirectToAction("Index");
            EmailCouponTOCustomer(details);
            return PreviewCoupon(details);
        }

        //Action shows the Preview of the Coupon
        public ActionResult PreviewCoupon(GenCouponViewModel model)
        {
            return View("PreviewCoupon",model);
        }

        //Sends Coupon to customer
        public ActionResult EmailCouponTOCustomer(GenCouponViewModel model)
        {
            try
            {
                var email = "";
                //1.Check if User is signed in
                //gets email address if user is, return 
                if (User.Identity.IsAuthenticated)
                    email = User.Identity.Name;
                else
                    return RedirectToAction("Index");


                //renders the coupon html to string
                string body = System.IO.File.ReadAllText(Server.MapPath("~/Views/Home/_PreCoupon.html"));

                //Replace dummy Value in Coupon Email Template
                body = body.Replace("#Offerer", model.OfferDetails.Merchant.CompanyName);
                body = body.Replace("#OfferName", model.OfferDetails.OfferName);
                body = body.Replace("#OfferEnds",model.OfferDetails.OfferEnds.ToString("MMM-dd-yyyy"));
                body = body.Replace("#OfferDetails", model.OfferDetails.OfferDetails);
                body = body.Replace("#DiscountRate%", model.OfferDetails.DiscountRate.ToString() +"%");
                body = body.Replace("#CouponCode", model.CouponCode);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(WebConfigurationManager.AppSettings["fromEmail"]);
                mail.Subject = "Veme Coupon";
                mail.Body = body;

                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;
                //mail.AlternateViews.Add(textView);

                string smtpHost = WebConfigurationManager.AppSettings["smtpHost"];
                string smtpAcc = WebConfigurationManager.AppSettings["smtpUser"];
                string smtpPassword = WebConfigurationManager.AppSettings["smtpPassword"];
                int smtpPort = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpPort"]);
                NetworkCredential cred = new NetworkCredential(smtpAcc, smtpPassword);

                using (SmtpClient mailClient = new SmtpClient(smtpHost, smtpPort))
                {
                    mailClient.EnableSsl = false;
                    mailClient.UseDefaultCredentials = false;
                    mailClient.Credentials = cred;
                    mail.To.Add("dwes_deomar@hotmail.com");
                    //mail.To.Add(email);
                    mailClient.Send(mail);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
            //returns nothing
            return null;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}