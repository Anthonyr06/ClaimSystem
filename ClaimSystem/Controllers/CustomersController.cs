using System;
using System.Collections.Generic;
using ClaimSystem.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClaimSystem.Models;
using ClaimSystem.Services;

namespace WebApplication1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly RepositoryEF<Customer> _customers;
        private readonly RepositoryEF<Address> _addresses;

        public CustomersController()
        {
            var dbContext = new AppDbContext();
            _customers = new RepositoryEF<Customer>(dbContext);
            _addresses = new RepositoryEF<Address>(dbContext);
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _customers.Get(null, null, "Address");
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customers.GetByID(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(_addresses.Get(), "AddressId", "Neighborhood");
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cedula,Name,LastName,Email,Password,PhoneNumber,AddressId")] Customer customer)
        {

            if (ModelState.IsValid)
            {
                var check = _customers.Get(e => e.Email == customer.Email.Trim().ToLower());

                if (check == null)
                {
                    customer.Password = MD5Service.GetMD5(customer.Password);
                    _customers.Insert(customer);
                    _customers.SaveObjects();
                    return RedirectToAction("Index", "Home");
                }

            }
            ViewBag.CustomerId = new SelectList(_addresses.Get(), "AddressId", "Neighborhood", customer.CustomerId);
            ModelState.AddModelError("", "El usuario ya existe");
            return View();
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customers.GetByID(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(_addresses.Get(), "AddressId", "Neighborhood", customer.CustomerId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cedula,Name,LastName,Email,Password,PhoneNumber,AddressId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customers.Update(customer);
                _customers.SaveObjects();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(_addresses.Get(), "AddressId", "Neighborhood", customer.CustomerId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customers.GetByID(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = _customers.GetByID(id);
            _customers.Delete(customer);
            _customers.SaveObjects();
            return RedirectToAction("Index");
        }

    }
}
