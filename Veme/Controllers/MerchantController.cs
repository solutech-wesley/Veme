using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Veme.Models;
using Veme.Models.POCO;
using Veme.Models.ViewModels;

namespace Veme.Controllers
{
    [Authorize(Roles = RoleName.Merchant)]
    public class MerchantController : Controller
    {
        private ApplicationDbContext _context;

        public MerchantController()
        {
            _context = new ApplicationDbContext();
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
        }

        // GET: Merchant
        public ActionResult Index()
        {
            return View();
        }


        //GET: // Create Offer
        public ActionResult CreateOffer()
        {
            var userId = User.Identity.GetUserId();
            var getUser = _context.Users.Include(c => c.Merchant).FirstOrDefault(c => c.Id == userId);

            var viewModel = new MerchantCreateOfferViewModel();
            viewModel.MerchantID = getUser.Merchant.MerchantID;
            viewModel.Merchant = getUser.Merchant;
            return View(viewModel);
        }

        //Get: Create Offer
        [HttpPost]
        public async Task<ActionResult> CreateOffer(MerchantCreateOfferViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                return await StoreOffer(model);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ActionResult> StoreOffer(MerchantCreateOfferViewModel model)
        {
            if (model == null)
                return RedirectToAction("CreateOffer");//return View("CreateOffer");

            if (model.OfferId.HasValue)
            {
                var editOffer = _context.Offers.FirstOrDefault(c => c.OfferId == model.OfferId);
                editOffer.DiscountRate = model.DiscountRate.Value;
                editOffer.OfferBegins = model.OfferBegins.Value;
                editOffer.OfferEnds = model.OfferEnds.Value;
                editOffer.OfferDetails = model.OfferDetails;
                editOffer.TotalOffer = model.TotalOffer;
                editOffer.OfferName = model.OfferName;
                editOffer.Merchant.MerchantID = model.Merchant.MerchantID;
                //uncomment to add offer image to database
                //editOffer.OfferImg = model.ConvertImgToByteArray();//convert image file to byte array
                editOffer.CouponDurationInMonths = model.CouponDurationInMonths.Value;
                editOffer.CouponPrice = model.CouponPrice.Value;
                editOffer.Merchant = model.Merchant;

                return RedirectToAction("CreateOffer");
            }

            //Create a new Offer
            var offerObj = new Offer()
            {
                DiscountRate = model.DiscountRate.Value,
                OfferBegins = model.OfferBegins.Value,
                OfferEnds = model.OfferEnds.Value,
                OfferDetails = model.OfferDetails,
                TotalOffer = model.TotalOffer,
                OfferName = model.OfferName,
                MerchantID = model.MerchantID,
                CreationDate = DateTime.Now.Date, //Set the creation date
                //uncomment to add offer image to database
                //OfferImg = model.ConvertImgToByteArray(),//convert image file to byte array
                CouponDurationInMonths = model.CouponDurationInMonths.Value,
                CouponPrice = model.CouponPrice.Value,
            };
            _context.Offers.Add(offerObj);
            _context.SaveChanges();
            
            //1. Ensures that production database has 
            //enough coupon codes generated
            await LoadProductionCoupon.LoadCoupons(model.TotalOffer.Value);

            return RedirectToAction("CreateOffer");
        }

        //Render the Page for Coupon Validation
        public ActionResult RedeemCoupon()
        {
            var userId = User.Identity.GetUserId();

            var getUser = _context.Users.Include(c => c.Merchant).FirstOrDefault(c => c.Id == userId);
            var viewModel = new MerchantRedeemViewModel()
            {
                MerchantId = getUser.Merchant.MerchantID
            };
            return View(viewModel);
        }

        //Method called via ajax to validate coupon
        public ActionResult Validate(MerchantRedeemViewModel viewModel)
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
        private string ValidateCoupon(MerchantRedeemViewModel model)
        {
            //1.Check if coupon code exist for this merchant
            var checkCoupon = _context.ProductionCodes.Include(c => c.Offers).FirstOrDefault(c => c.CouponCode.Replace("-", "") == model.CouponCode.Replace("-", "") && c.IsUsed == false && c.IsActive == true && c.Offers.MerchantID == model.MerchantId);

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


    }
}