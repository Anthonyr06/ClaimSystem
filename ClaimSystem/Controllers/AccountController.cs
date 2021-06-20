using ClaimSystem.Models;
using ClaimSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
      //  private readonly RepositoryEF<Customer> _customer;

        public AccountController()
        {
            var dbContext = new Data.AppDbContext();

            _employees = new RepositoryEF<Employee>(dbContext);
        //    _customer = new RepositoryEF<Customer>(dbContext);
        }

        public ActionResult Index()
        {                
            return RedirectToAction("Login");
        }

        //public ActionResult CreateCustomer()
        //{
        //    return View();
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateCustomer(Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var check = _customer.Get(e => e.Email == customer.Email.Trim().ToLower());

        //        if (check == null)
        //        {
        //            customer.Password = MD5Service.GetMD5(customer.Password);
        //            _customer.Insert(customer);
        //            _customer.SaveObjects();
        //            return RedirectToAction("Index", "Home");
        //        }

        //    }
        //    ModelState.AddModelError("", "El usuario ya existe");
        //    return View();
        //}


        public ActionResult Login()
        {
            var test = MD5Service.GetMD5("12345"); //827ccb0eea8a706c4c34a16891f84e7b
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = MD5Service.GetMD5(password);
                var employee = _employees.Get(s => s.Email.Equals(email.Trim().ToLower()) && s.Password.Equals(f_password)).FirstOrDefault();
                if (employee != null)
                {
                    FormsAuthentication.SetAuthCookie(employee.Email, false);
                    return RedirectToAction("Index","Home");
                }
            }
            ModelState.AddModelError("", "Email o contraseña invalida");
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        //public static string GetMD5(string str)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] fromData = Encoding.UTF8.GetBytes(str);
        //    byte[] targetData = md5.ComputeHash(fromData);
        //    string byte2String = null;

        //    for (int i = 0; i < targetData.Length; i++)
        //    {
        //        byte2String += targetData[i].ToString("x2");

        //    }
        //    return byte2String;
        //}

    }
}