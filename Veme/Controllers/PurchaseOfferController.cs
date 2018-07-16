using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Veme.Models;
using Veme.Models.ViewModels;
using System.Data.Entity;
using System.Threading.Tasks;
using Veme.Models.POCO;

namespace Veme.Controllers
{
    public class PurchaseOfferController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: PurchaseOffer
        //[ActionName("purchase-coupon")]
        public PurchaseOfferController()
        {
            //Needed for to enable Client Side Validation at application level
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
            //HtmlHelper.ClientValidationEnabled = true;
        }

        //Render the Purchase Coupon Page
        //Allow All merchant to be listed
        public async Task<ActionResult> Index()
        {
            var getMerchants = _context.Merchants.ToList();
            var viewModel = new PurchaseCouponViewModel()
                                { Merchants = getMerchants,
                                  Offer = new List<Offer>()};
            return View(viewModel);
        }

        //Action to puchase the Coupon
        [HttpPost]
        public async Task<ActionResult> Purchase(PurchaseCouponViewModel model)
        {            
            if (!ModelState.IsValid)
                return RedirectToAction("Index",model);

            var getOffer = _context.Offers.FirstOrDefault(c => c.OfferId == model.OfferId);

            var getRandomProductionCode = _context.ProductionCodes.Where(c => c.IsActive == false && c.IsUsed == false && c.OfferId == null).OrderBy(c => Guid.NewGuid()).FirstOrDefault();

            if (getRandomProductionCode != null)
            {
                getRandomProductionCode.OfferId = model.OfferId;
                getRandomProductionCode.IsActive = true;
                _context.SaveChanges();
            }

            var details = new GenCouponViewModel()
            {
                OfferDetails = getOffer,
                CouponCode = getRandomProductionCode.CouponCode
            };
            //return View();
            return GenerateCoupon(details);
        }

        //Returns a Page with the generated Coupon
        public ActionResult GenerateCoupon(GenCouponViewModel model)
        {
            return View("GenerateCoupon",model);
        }
        //Render the Page for Coupon Redemtion
        public ActionResult RedeemCoupon()
        {
            var viewModel = new AdminCouponViewModel()
            {
                Merchants = _context.Merchants.ToList()
            };
            return View(viewModel);
        }

        //Method called via ajax to validate coupon
        public ActionResult Validate(AdminCouponViewModel viewModel)
        {
            dynamic showMessage = string.Empty;
            #region
            //if (!ModelState.IsValid)
            //{
            //    showMessage = new
            //    {
            //        statusCode = 404,
            //        Message = "Oops!"
            //    };
            //    return Json(showMessage, JsonRequestBehavior.AllowGet);
            //}
            #endregion
            var getResults = ValidateCoupon(viewModel);
            showMessage = new
            {
                statusCode = 200,
                Message = getResults//"Coupon Valid"
            };
            return Json(data: showMessage, behavior: JsonRequestBehavior.AllowGet);
        }

        //MEthod containing the logics of checking coupon
        [NonAction]
        private string ValidateCoupon(AdminCouponViewModel model)
        {
            var checkCoupon = _context.ProductionCodes.Include(c => c.Offers).FirstOrDefault(c => c.CouponCode.Replace("-", "") == model.CouponCode.Replace("-","") && c.IsUsed ==false && c.IsActive == true);

            //vlaidates if code exist
            if (checkCoupon == null)
                return "Invalid Coupon";

            //Check if max coupons used
            if (checkCoupon.Offers.TotalOffer == checkCoupon.Offers.CouponUsed)
                return "Offer Coupon Exhausted";

            //Increment coupons used
            checkCoupon.Offers.CouponUsed += 1;
            checkCoupon.IsUsed = true;

            //save changes to the database
            _context.SaveChanges();

            return "Valid Coupon";
        }

        //Populate the Offer ddlist by merchant ID
        //[HttpPost]
        //public ActionResult GetOffersByMerchant(int MerchantId)
        //{
        //    return Json(new SelectList(_context.Offers.Where(c => c.MerchantID == MerchantId), "OfferId", "OfferName"));
        //}
        //Populate the Offer ddlist by merchant ID
        [HttpPost]
        public ActionResult GetOffersByMerchant(string MerchantId)
        {
            return Json(new SelectList(_context.Offers.Where(c => c.MerchantID == MerchantId), "OfferId", "OfferName"));
        }
    }
}