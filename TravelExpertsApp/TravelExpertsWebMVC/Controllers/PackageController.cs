using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;
using TravelExpertsDB;
using TravelExpertsWebMVC.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TravelExpertsWebMVC.Controllers
{
    public class PackageController : Controller
    {
        private TravelExpertsContext? db { get; set; }
        // added constructor
        public PackageController(TravelExpertsContext db)
        {
            this.db = db;
        }

        // [HttpGet]
        // Returns view with all avaliable packages
        public ActionResult Index(int id)
        {
            List<Package> packages = PackageDB.GetPackages(db!);
            return View(packages);
        }

        [Authorize]
        // [HttpGet]
        // Return view of booking page. View is authorized to validated users.
        public ActionResult Book(int id) // package id passed from view
        {
            Package? package = null;
            try
            {
                package = PackageDB.GetPackageById(db!, id); // get package data of package with matching packageid
                int? customerId = HttpContext.Session.GetInt32("CustomerId");
                if (package != null && customerId != null)
                {
                    TempData["PackageId"] = package.PackageId;
                    PrebookingViewModel viewModel = new PrebookingViewModel();
                    viewModel.PkgName = package.PkgName;
                    viewModel.PkgDesc = package.PkgDesc;
                    viewModel.PkgStartDate = package.PkgStartDate;
                    viewModel.PkgEndDate = package.PkgEndDate;
                    viewModel.PkgBasePrice = package.PkgBasePrice;
                    viewModel.PackageId = package.PackageId;
                    viewModel.CustomerId = (int)customerId;
                    List<TripType> types = BookingDB.GetTripTypes(db!);
                    ViewBag.Types = new SelectList(types);
                    viewModel.TripTypeId = types.FirstOrDefault()?.Ttname.Substring(0, 1);
                    return View(viewModel); // return the TripTypeId to the view

                }
            }
            catch (Exception)
            {
                TempData["Message"] = "Database connection error. Try again later.";
                TempData["IsError"] = true;
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateBooking(PrebookingViewModel viewModel)
        {
            if (viewModel.TravelerCount != null && viewModel.TravelerCount > 0)
            {
                int? customerId = HttpContext.Session.GetInt32("CustomerId");
                Booking booking = BookingDB.CreateBooking((int)customerId, viewModel.PackageId, (int)viewModel.TravelerCount, viewModel.TripTypeId);
                BookingDB.SaveBooking(db, booking);
                int bookingId = booking.BookingId;
                return RedirectToAction("Confirmation", "Package", new { id = bookingId });
            }
            else
            {
                return RedirectToAction("Index", "Package");
            }
        }

        //[HttpGet]
        // Returns view of Booking confirmation displaying all booking details
        public ActionResult Confirmation(int id)
        {
            OrderDB orderDb = new OrderDB();
            OrderDTO order = orderDb.GetOrderDetails(db, id);
            return View(order);
        }
        /// <summary>
        /// Called when the number of travelers is changed. Returns a view component that updates the total cost of the package 
        /// </summary>
        /// <author>Michael Chessall</author>
        [HttpPost]
        [Authorize]
        public ActionResult ChangeNum(decimal baseP, int numT)
        {
            numT = Math.Max(numT, 1);

            return ViewComponent("PrebookingPrice", new { basePrice = baseP, numTravel = numT });

        }
    }
}
