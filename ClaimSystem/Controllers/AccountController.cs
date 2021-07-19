using ClaimSystem.Models;
using ClaimSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ClaimSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly RepositoryEF<Employee> _employees;
        private readonly RepositoryEF<Customer> _customer;

        public AccountController()
        {
            var dbContext = new Data.AppDbContext();

            _employees = new RepositoryEF<Employee>(dbContext);
            _customer = new RepositoryEF<Customer>(dbContext);
        }

        public ActionResult Index()
        {                
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login data)
        {
            if (ModelState.IsValid)
            {
                var f_password = MD5Service.GetMD5(data.Password);

                var employee = _employees.Get(s => s.Email.Equals(data.Email.Trim().ToLower()) && s.Password.Equals(f_password)).FirstOrDefault();
                
                if (employee != null)
                {
                    //ClaimsPrincipal.Current.Identities.First().AddClaim(
                    //    new System.Security.Claims.Claim(ClaimTypes.Role, nameof(Employee)));

                    //FormsAuthentication.SetAuthCookie(data.Email, false);

                    SetCookie(data.Email, nameof(Employee));
                    return RedirectToAction("Index", "Home");
                }

                var customer = _customer.Get(s => s.Email.Equals(data.Email.Trim().ToLower()) && s.Password.Equals(f_password)).FirstOrDefault();
                if (customer != null)
                {
                    //ClaimsPrincipal.Current.Identities.First().AddClaim(
                    //    new System.Security.Claims.Claim(ClaimTypes.Role, nameof(Customer)));

                    //FormsAuthentication.SetAuthCookie(data.Email, false);

                    SetCookie(data.Email, nameof(Customer));
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Email o contraseña invalida");
            return View();
        }

        private void SetCookie(string Username, string role)
        {
            // Create a new ticket used for authentication
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
               1, // Ticket version
               Username, // Username associated with ticket
               DateTime.Now, // Date/time issued
               DateTime.Now.AddMinutes(30), // Date/time to expire
               true, // "true" for a persistent user cookie
               role, // User-data, in this case the roles
               FormsAuthentication.FormsCookiePath);// Path cookie valid for

            // Encrypt the cookie using the machine key for secure transport
            string hash = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(
               FormsAuthentication.FormsCookieName, // Name of auth cookie
               hash); // Hashed ticket

            // Set the cookie's expiration time to the tickets expiration time
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

            // Add the cookie to the list for outgoing response
            Response.Cookies.Add(cookie);
        }

        //Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Admin()
        {
            Employee employee = _employees.Get(e => e.Email == User.Identity.Name).FirstOrDefault();
            Customer customer = _customer.Get(c => c.Email == User.Identity.Name).FirstOrDefault();

            Login data =  new Login();

            if (customer != null)
                data.Email = customer.Email;
            else if (employee != null)
                data.Email = employee.Email;
            else
                return HttpNotFound();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin(Login data)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employees.Get(e => e.Email == User.Identity.Name).FirstOrDefault();
                Customer customer = _customer.Get(s => s.Email == User.Identity.Name).FirstOrDefault();

                Login d = new Login();

                if (customer != null)
                {
                    customer.Email = data.Email.Trim().ToLower();
                    customer.Password = MD5Service.GetMD5(data.Email);

                    _customer.Update(customer);
                    _customer.SaveObjects();
                }
                if (employee != null)
                {
                    employee.Email = data.Email.Trim().ToLower();
                    employee.Password = MD5Service.GetMD5(data.Email);

                    _employees.Update(employee);
                    _employees.SaveObjects();
                }
                else
                    return HttpNotFound();

                    return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}