using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelExpertsDB;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TravelExpertsWebMVC.Models;

namespace TravelExpertsMVC.Controllers
{
    public class AccountController : Controller
    {
        private TravelExpertsContext? db { get; set; }
        // added constructor
        public AccountController(TravelExpertsContext db)
        {
            this.db = db;
        }
        public IActionResult LoginAsync(string returnUrl = "")
        {
            if (returnUrl != null && returnUrl != "")
            {
                TempData["ReturnUrl"] = returnUrl;

            }
            return View();
        }

        // if a method is Asyncronous, it needs to return the task rather than a type.
        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer cust) // data collected from the form "user"
        {
            Customer? cst = CustomerManager.Authenticate(db, cust.CustEmail, cust.Password);
            if (cst == null) // if there are no matching username + password combinations
            {
                TempData["LoginButtonClicked"] = true; // displays a warning message "password or username was incorrect".
                return View(); // stay on the login page
            }
            if (cst != null) // authenticated customer exists
            {
                HttpContext.Session.SetInt32("CustomerId", cst.CustomerId); // set the Session CustomerID to the authenticated customer.
            }

            // 1. create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim("Email", cst.CustEmail),
                new Claim(ClaimTypes.Name, cst.CustFirstName)
            };
            // 2. create claims identity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme); // cookies authentication
            // 3. create claims pricipal
            ClaimsPrincipal claimsPricipal = new ClaimsPrincipal(claimsIdentity);

            // ready for signing in
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPricipal);
            // redirect to the protected page that initiated the login sequence if defined
            string? returnUrl = TempData["ReturnUrl"]?.ToString();
            if (string.IsNullOrEmpty(returnUrl)) // if not return URL
            {
                return RedirectToAction("Index", "Home"); // Return to the Home page
            }
            else
            {
                return Redirect(returnUrl); // Return to the page that initiated the login
            }
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("CustomerId"); // remove OwnerId from session state when logging out
            return RedirectToAction("Index", "Home"); // go to home page
        }

        //[HttpGet]
        public ActionResult Register(string id = "Select")
        {
            return View(new Customer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Customer newCustomerData)
        {
            if (CustomerManager.EmailExists(db, newCustomerData.CustEmail))
            {
                ModelState.AddModelError("CustEmail", $"This email {newCustomerData.CustEmail} is already registered to an account.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CustomerManager.CreateCustomer(db, newCustomerData);
                    TempData["Message"] = $"Thank you for registering {newCustomerData.CustFirstName} {newCustomerData.CustLastName}!";

                    // Log in the user after successful registration
                    await LoginAsync(newCustomerData);
                }
                catch (Exception)
                {
                    TempData["Message"] = "There was a problem with registering. Please try again later.";
                    TempData["IsError"] = true;
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(newCustomerData);
            }
        }

        //[HttpGet]
        public ActionResult Account()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            Customer customer = CustomerManager.GetCustomerData(db!, customerId);
            return View(customer);

        }

        //[HttpGet]
        // Please use the same format as "Account.cshtml". Just replace the content inside of div id="account-info" to keep the style consistent.
        public ActionResult OrderHistory()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if(customerId != null)
            {
                List<Booking> bookings = BookingDB.GetAllBookings(db!, (int)customerId);
                return View(bookings);
            }
            else
            {
                return View();
            }
        }

        public ActionResult RegisteredAgent()
        {
            int? CustomerId = HttpContext.Session.GetInt32("CustomerId");

            if (CustomerId != null)
            {
                Customer ActiveUser = db.Customers.First(Item => Item.CustomerId == CustomerId);

                if(ActiveUser.AgentId == null) { return View(); }

                Agent DisplayMe = db.Agents.First(Item => Item.AgentId == ActiveUser.AgentId);
                return View(DisplayMe);
            }
            else
            {
                return View();
            }
        }

        public ActionResult ReleaseAgent()
        {
            int? CustomerId = HttpContext.Session.GetInt32("CustomerId");

            if (CustomerId != null)
            {
                Customer ActiveUser = db.Customers.First(Item => Item.CustomerId == CustomerId);
                ActiveUser.AgentId = null;
                db.Customers.Update(ActiveUser);
                db.SaveChanges();
            }

            return RedirectToAction("RegisteredAgent", "Account");
        }

        public ActionResult OrderHistoryDetails(int id)
        {
            OrderDB orderDb = new OrderDB();
            OrderDTO order = orderDb.GetOrderDetails(db!, id);

            return View(order);
        }
        [HttpPost]
        public ActionResult MakePayment(int? bookid)
        {
            BookingPaymentViewModel model = new();
            Booking booking = BookingDB.FindBooking(db, bookid);
            model.PkgName = booking.Package.PkgName;
            model.BookingDate = booking.BookingDate;
            booking.TotalPaid ??= 0;
            model.Balance = (decimal)booking.Package.PkgBasePrice * (decimal)booking.TravelerCount - booking.TotalPaid;
            return View(model);
        }

        //[HttpGet]
        public ActionResult Edit()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            Customer customer = CustomerManager.GetCustomerData(db!, customerId);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer editedCustomer)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            if (CustomerManager.NewEmailExists(db, customerId, editedCustomer.CustEmail!))
            {
                ModelState.AddModelError("CustEmail", $"This email {editedCustomer.CustEmail} is already registered to another account.");
            }
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                try
                {
                    CustomerManager.UpdateCustomer(db, (int)customerId, editedCustomer);
                    TempData["Message"] = $"Your account was successfully updated";
                }
                catch (Exception)
                {
                    TempData["Message"] = "There was a problem with updating your account. Please try again later.";
                    TempData["IsError"] = true;
                }
                return RedirectToAction("Account", "Customer");
            }
            else
            {
                return View(editedCustomer);
            }
        }
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Customer withNewPassword)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            ModelState.Remove("CustFirstName");
            ModelState.Remove("CustLastName");
            ModelState.Remove("CustAddress");
            ModelState.Remove("CustCity");
            ModelState.Remove("CustProv");
            ModelState.Remove("CustPostal");
            ModelState.Remove("CustCountry");
            ModelState.Remove("CustHomePhone");
            ModelState.Remove("CustBusPhone");
            ModelState.Remove("CustEmail");
            ModelState.Remove("Agent");
            string newPassword = withNewPassword.Password;
            if (ModelState.IsValid)
            {
                try
                {
                    CustomerManager.UpdatePassword(db!, customerId, newPassword);
                    TempData["Message"] = $"Your password was successfully changed";
                }
                catch (Exception)
                {
                    TempData["Message"] = "There was a problem with changing your password. Please try again later.";
                    TempData["IsError"] = true;
                    return View();
                }
            }
            return RedirectToAction("Account", "Customer");
        }
    }
}