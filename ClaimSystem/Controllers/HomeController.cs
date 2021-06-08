using ClaimSystem.Models;
using ClaimSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClaimSystem.Controllers
{
    public class HomeController : Controller
    {
       private readonly RepositoryEF<Address> _addresses;

       public HomeController()
       {
            var dbContext = new Data.AppDbContext();

           _addresses = new RepositoryEF<Address>(dbContext);

        }

        // GET: Addresses
        public ActionResult Index()
        {
            var t = _addresses.GetByID(1);
            return View();
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