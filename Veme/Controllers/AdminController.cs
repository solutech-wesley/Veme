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
    //[Authorize(Roles = RoleName.Admin)]
    [AuthorizeRoles(RoleName.Admin,RoleName.Merchant)]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //GET:Create Offer
        public ActionResult CreateOffer()
        {
            OfferModel viewModel = new OfferModel();
            viewModel.Merchants = _context.Merchants.ToList();
            viewModel.OFFERID = null;
            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult UpdateOffer(OfferModel model)
        {
            if(model.OFFERID != null)
            {
                //var updateOffer = _context.Offers.Single(c => c.OfferId == model.OffererId);
                var updateOffer = _context.Offers.Single(c => c.OfferId == model.OFFERID);
                
                updateOffer.DiscountRate = model.Discount;
                //MerchantName = offerModel.Merchants.ToString(),
                updateOffer.OfferBegins = model.OfferStarts.Value;
                updateOffer.OfferEnds = model.OfferEnds.Value;
                updateOffer.OfferDetails = model.OfferDescription;
                updateOffer.TotalOffer = model.TotalOffers;
                updateOffer.OfferName = model.OfferName;
                updateOffer.MerchantID = model.OffererId;

                _context.SaveChanges();
                return RedirectToAction("CreateOffer");
            }
            else
                return View("CreateOffer");
        }
        [HttpPost]
        public ActionResult CreateOffer(OfferModel offerModel)
        {
            if (!ModelState.IsValid)
                return View(offerModel);

            var merchant = _context.Merchants.FirstOrDefault(c => c.MerchantID == offerModel.OffererId);

            if (merchant != null)
                offerModel.MerchantName = merchant.CompanyName;
            Session["couponDetails"] = offerModel;
            //return RedirectToAction("Preview",offerModel);
            return Preview(offerModel);
        }

        //[HttpGet]
        //public ActionResult Preview()
        //{
        //    return View();
        //}

        public ActionResult Preview(OfferModel model)
        {
            if(model.OfferEnds.HasValue)
            {
                model.FormatDate = model.OfferEnds.Value.ToString("MMM/dd/yyyy");
            }
            return View("Preview",model);
        }


        //public ActionResult StoreOffer(OfferModel offerModel)
        public async Task<ActionResult> StoreOffer()
        {
            var offerModel = Session["couponDetails"] as OfferModel;

            if (offerModel == null)
                return View("CreateOffer");

            var offerObj = new Offer()
            {
                DiscountRate = offerModel.Discount,
                //MerchantName = offerModel.Merchants.ToString(),
                OfferBegins = offerModel.OfferStarts.Value,
                OfferEnds = offerModel.OfferEnds.Value,
                OfferDetails = offerModel.OfferDescription,
                TotalOffer = offerModel.TotalOffers,
                OfferName = offerModel.OfferName,
                MerchantID = offerModel.OffererId
            };

            //Allot Coupons to Production

             await LoadProductionCoupon.LoadCoupons(offerModel.TotalOffers);
            _context.Offers.Add(offerObj);
            _context.SaveChanges();

            return RedirectToAction("CreateOffer", "Admin");
        }

        [HttpGet]
        public ActionResult EditOffer()
        {
            var viewModel = new EditOfferViewModel()
            {
                Merchants = _context.Merchants.ToList(),
                Offers = new List<Offer>()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditOffer(EditOfferViewModel model)
        {
            var getOfferToEdit = _context.Offers.FirstOrDefault(c => c.OfferId == model.Offer_Id);

            if (getOfferToEdit != null)
                getOfferToEdit.Merchant = _context.Merchants.FirstOrDefault(c => c.MerchantID == model.MerchantID);
            if (!ModelState.IsValid)
                return View(model);

            var offerModel = new OfferModel()
            {
                Merchants = _context.Merchants.ToList(),
                Discount = getOfferToEdit.DiscountRate,
                OfferDescription = getOfferToEdit.OfferDetails,
                MerchantName = getOfferToEdit.Merchant.CompanyName,
                OffererId = getOfferToEdit.Merchant.MerchantID,
                OfferName = getOfferToEdit.OfferName,
                OfferStarts = getOfferToEdit.OfferBegins,
                OfferEnds = getOfferToEdit.OfferEnds,
                TotalOffers = (int) getOfferToEdit.TotalOffer,
                OFFERID = getOfferToEdit.OfferId

            };
            //return View();
            return View("CreateOffer", offerModel);
        }

        ////Populate the Offer ddlist by merchant ID
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
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}