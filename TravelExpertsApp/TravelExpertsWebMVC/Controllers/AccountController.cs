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
        
        // Task to log out the user
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("CustomerId"); // remove OwnerId from session state when logging out
            return RedirectToAction("Index", "Home"); // go to home page
        }

        //[HttpGet]
        // Register page with new Customer object.
        public ActionResult Register(string id = "Select")
        {
            return View(new Customer()); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Customer newCustomerData)
        {
            // Check the email provided on the form for matching email in the database.
            // Because the email is used in place of a Username, it must be unique to each account.
            if (CustomerManager.EmailExists(db, newCustomerData.CustEmail)) // if there is a matching email, ModelState is not valid.
            {
                ModelState.AddModelError("CustEmail", $"This email {newCustomerData.CustEmail} is already registered to an account.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Create and save customer to database.
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
                return RedirectToAction("Index", "Home"); // return to home page after successful registration
            }
            else
            {
                return View(newCustomerData); // stay on register page with existing form inputs
            }
        }

        //[HttpGet]
        // Returns Account page displaying Session user data.
        public ActionResult Account()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            Customer customer = CustomerManager.GetCustomerData(db!, customerId);
            return View(customer);
        }

        //[HttpGet]
        // Returns Booking History page with all bookings made by session user
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

        //[HttpGet]
        // Returns Booking details of selected booking from booking history page.
        public ActionResult OrderHistoryDetails(int id) // selected booking id
        {
            OrderDB orderDb = new OrderDB(); // class with methods for working with OrderDTO objects
            OrderDTO order = orderDb.GetOrderDetails(db!, id);

            return View(order);
        }

        //[HttpGet]
        // Returns view with details of selected booking asking for confirmation.
        public ActionResult CancelBooking(int id)
        {
            OrderDB orderDb = new OrderDB();
            OrderDTO order = orderDb.GetOrderDetails(db!, id);

            return View(order);
        }

        [HttpPost]
        // Removes selected Booking from Bookings table.
        public ActionResult CancelBooking(int? bookid)
        {
            // if there is no booking id passed from the view.
            if(bookid == null)
            {
                TempData["Message"] = "Could not find the booking. Please try again.";
                TempData["IsError"] = true;
                return RedirectToAction("OrderHistory", "Account"); // redirect user to account order history page
            }
            int bookingId = bookid.Value;

            Booking booking = BookingDB.FindBooking(db!, bookingId);
            // if there is no bookings with the provided BookingID.
            if(booking == null)
            {
                TempData["Message"] = "Could not find the booking. Please try again.";
                TempData["IsError"] = true;
                return RedirectToAction("OrderHistory", "Account"); // redirect user to account order history page
            }

            bool success = BookingDB.DeleteBooking(db!, booking); // DeleteBooking returns true if booking is removed successfully.

            if(success == true)
            {
                TempData["Message"] = $"Booking {bookingId} has been successfully cancelled.";
                return RedirectToAction("OrderHistory", "Account"); // redirect user to account order history page with confirmation message
            }
            else // stay on the cancel booking page with error message
            {
                TempData["Message"] = "There was a problem with cancelling. Please try again later.";
                TempData["IsError"] = true;
            }
            return View();
        }

        /// <summary>
        /// Opens the MakePayment screen for the passed bookid
        /// </summary>
        /// <author>Michael Chessall</author>
        [HttpPost]
        public ActionResult MakePayment(int? bookid)
        {
            BookingPaymentViewModel model = new();
            Booking booking = BookingDB.FindBooking(db, bookid);
            model.PkgName = booking.Package.PkgName;
            model.BookingDate = booking.BookingDate;
            booking.TotalPaid ??= 0;
            model.Balance = (decimal)booking.Package.PkgBasePrice * (decimal)booking.TravelerCount - booking.TotalPaid;
            model.BookingId = booking.BookingId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmPayment(BookingPaymentViewModel model)
        {
            if (model.Payment > model.Balance)
            {
                ModelState.AddModelError("Payment", $"You cannot make a payment larger than the balance.");
            }

            if (model.Payment <= 0)
            {
                ModelState.AddModelError("Payment", $"Payments must be greater than 0.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Booking booking = BookingDB.FindBooking(db, model.BookingId);
                    if (booking.TotalPaid == null) booking.TotalPaid = 0;
                    booking.TotalPaid += model.Payment;
                    db.SaveChanges();


                }
                catch (Exception e)
                {
                    TempData["Message"] = "There was a problem with payment. Please try again later." + model.BookingId;
                    TempData["IsError"] = true;
                }
                return RedirectToAction("OrderHistory", "Account");
            }
            else
            {
                return View("MakePayment", model);
            }
        }

        //[HttpGet]
        // Returns view of form with users data pre filled in the input areas.
        public ActionResult Edit()
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            Customer customer = CustomerManager.GetCustomerData(db!, customerId);
            return View(customer);
        }

        [HttpPost]
        // Saves changes on customer account after successful validation and updates the database.
        public ActionResult Edit(Customer editedCustomer)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            // checks if the updated email already exists in the database, other than the current email the customer is using.
            if (CustomerManager.NewEmailExists(db, customerId, editedCustomer.CustEmail!))
            {
                ModelState.AddModelError("CustEmail", $"This email {editedCustomer.CustEmail} is already registered to another account.");
            }
            // Password is not updated on this form. So it is removed from the ModelState to pass validation.
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
                return RedirectToAction("Account", "Account"); // redirect customer to account details page.
            }
            else
            {
                return View(editedCustomer); // stay on page with current inputs in the case update is not successful
            }
        }

        // [HttpGet]
        // Returns view of empty form with Password and ConfirmPassword inputs
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        // Updates the users password if the model state is valid
        public ActionResult ChangePassword(Customer withNewPassword)
        {
            int? customerId = HttpContext.Session.GetInt32("CustomerId");
            // remove everything but Password and ConfirmPassword from the Customer Model State.
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
                    return View(); // stay on the current page if change is unsuccessful.
                }
            }
            return RedirectToAction("Account", "Account"); // return account details page when change is successful.
        }
    }
}