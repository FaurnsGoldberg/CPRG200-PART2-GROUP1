//Author: Owen Huot
//Date: 14.02.24
//Description: functionality to register with agents
//wasn't required of workshop, but there weren't enough tasks to go around otherwise

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using TravelExpertsDB;
using TravelExpertsWebMVC.Models;

namespace TravelExpertsWebMVC.Controllers
{
    public class AgentsController : Controller
    {
        private TravelExpertsContext? Context { get; set; }

        public AgentsController(TravelExpertsContext Context)
        {
            this.Context = Context;
        }

        public IActionResult Index()
        {
            List<Agent> packages = Context.Agents.ToList();
            return View(packages);
        }

        public ActionResult RegisterAgent(int id)
        {
            int? Customer = HttpContext.Session.GetInt32("CustomerId");

            if (Customer != null)
            {
                Customer RegisterMe = Context.Customers.First(Item => Item.CustomerId == Customer);
                RegisterMe.AgentId = id;
                Context.Customers.Update(RegisterMe);
                Context.SaveChanges();

                return RedirectToAction("RegisteredAgent", "Account");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
