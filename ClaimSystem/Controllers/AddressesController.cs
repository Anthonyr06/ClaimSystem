using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClaimSystem.Data;
using ClaimSystem.Models;
using ClaimSystem.Services;

namespace ClaimSystem.Controllers
{
    [Authorize(Roles = nameof(Employee))]
    public class AddressesController : Controller
    {
        private readonly RepositoryEF<Address> _addresses;

        public AddressesController()
        {
            var dbContext = new AppDbContext();
            _addresses = new RepositoryEF<Address>(dbContext);
        }

        // GET: Addresses
        public ActionResult Index()
        {
            var addresses = _addresses.Get();
            return View(addresses);
        }

        // GET: Addresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = _addresses.GetByID(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // GET: Addresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AddressId,AddressType,Number,Neighborhood,City,Country")] Address address)
        {
            if (ModelState.IsValid)
            {
                _addresses.Insert(address);
                _addresses.SaveObjects();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Addresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = _addresses.GetByID(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Address address)
        {
            if (ModelState.IsValid)
            {
                Address a = _addresses.GetByID(address.AddressId);
                a.AddressType = address.AddressType;
                a.City = address.City;
                a.Country = address.Country;
                a.Neighborhood = address.Neighborhood;
                a.Number = address.Number;

                _addresses.Update(a);
                _addresses.SaveObjects();
                return RedirectToAction("Index");
            }
            return View(address);
        }

        // GET: Addresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = _addresses.GetByID(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = _addresses.GetByID(id);
            _addresses.Delete(address);
            _addresses.SaveObjects();
            return RedirectToAction("Index");
        }
    }
}
