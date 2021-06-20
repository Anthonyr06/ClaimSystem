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

namespace ClaimSystem.Controllers
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
            Customer customer = _customers.Get(c => c.CustomerId == id, null, "Address").FirstOrDefault();
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(_addresses.Get(), "AddressId", nameof(Address.FullAddress));
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

                if (!check.Any())
                {
                    customer.Password = MD5Service.GetMD5(customer.Password);
                    customer.Address = _addresses.GetByID(customer.AddressId);
                    _customers.Insert(customer);
                    _customers.SaveObjects();
                    return RedirectToAction("Index");
                }

            }
            ViewBag.AddressId = new SelectList(_addresses.Get(), "AddressId", nameof(Address.FullAddress), customer.AddressId);
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
            ViewBag.AddressId = new SelectList(_addresses.Get(), "AddressId", nameof(Address.FullAddress), customer.AddressId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                Customer c = _customers.GetByID(customer.CustomerId);
                c.Cedula = customer.Cedula;
                c.Name = customer.Name;
                c.LastName = customer.LastName;
                c.Email = customer.Email;
                c.PhoneNumber = customer.PhoneNumber;
                c.AddressId = customer.AddressId;

                _customers.Update(c);
                _customers.SaveObjects();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(_addresses.Get(), "AddressId", nameof(Address.FullAddress), customer.AddressId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = _customers.Get(c => c.CustomerId == id, null, "Address").FirstOrDefault();
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
