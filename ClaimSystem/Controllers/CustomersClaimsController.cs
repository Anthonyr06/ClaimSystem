using ClaimSystem.Data;
using ClaimSystem.Models;
using ClaimSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ClaimSystem.Controllers
{
    [Authorize(Roles = nameof(Customer))]
    public class CustomersClaimsController : Controller
    {
        private readonly RepositoryEF<Claim> _claims;
        private readonly RepositoryEF<ClaimPriority> _claimPriorities;
        private readonly RepositoryEF<ClaimState> _claimsStates;
        private readonly RepositoryEF<ClaimType> _claimsTypes;
        private readonly RepositoryEF<Customer> _customers;
        private readonly RepositoryEF<Employee> _employees;

        public CustomersClaimsController()
        {
            var dbContext = new AppDbContext();
            _claims = new RepositoryEF<Claim>(dbContext);
            _customers = new RepositoryEF<Customer>(dbContext);
            _employees = new RepositoryEF<Employee>(dbContext);
            _claimPriorities = new RepositoryEF<ClaimPriority>(dbContext);
            _claimsStates = new RepositoryEF<ClaimState>(dbContext);
            _claimsTypes = new RepositoryEF<ClaimType>(dbContext);
        }

        public ActionResult Index()
        {
            var claims = _claims.Get(c => c.Customer.Email == User.Identity.Name, c => c.OrderBy(cs => cs.ClaimState.Desc), "ClaimPriority,ClaimState,ClaimType,Customer,Employee");
            return View(claims);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim Claim = _claims.Get(c => c.Customer.Email == User.Identity.Name && c.ClaimId == id, null, "ClaimPriority,ClaimState,ClaimType,Customer,Employee").FirstOrDefault();
            if (Claim == null)
            {
                return HttpNotFound();
            }
            return View(Claim);
        }

        public ActionResult Create()
        {
            ViewBag.ClaimTypeId = new SelectList(_claimsTypes.Get(), "ClaimTypeId", "Desc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Dish,Desc,ClaimTypeId")] Claim Claim)
        {
            if (ModelState.IsValid)
            {
                Claim.StartDate = DateTime.Now;
                _claims.Insert(Claim);
                _claims.SaveObjects();
                return RedirectToAction("Index");
            }

            ViewBag.ClaimTypeId = new SelectList(_claimsTypes.Get(), "ClaimTypeId", "Desc", Claim.ClaimTypeId);
            return View(Claim);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim Claim = _claims.Get(c => c.Customer.Email == User.Identity.Name && c.ClaimId == id, null, null).FirstOrDefault();
            if (Claim == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClaimTypeId = new SelectList(_claimsTypes.Get(), "ClaimTypeId", "Desc", Claim.ClaimTypeId);
            return View(Claim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Claim Claim)
        {
            if (ModelState.IsValid)
            {
                Claim c = _claims.Get(cl => cl.Customer.Email == User.Identity.Name && cl.ClaimId == Claim.ClaimId, null, null).FirstOrDefault();
                c.Dish = Claim.Dish;
                c.Desc = Claim.Desc;
                c.ClaimTypeId = Claim.ClaimTypeId;
                c.Customer = _customers.Get(cu => cu.Email == User.Identity.Name).FirstOrDefault();

                _claims.Update(c);
                _claims.SaveObjects();
                return RedirectToAction("Index");
            }
            ViewBag.ClaimTypeId = new SelectList(_claimsTypes.Get(), "ClaimTypeId", "Desc", Claim.ClaimTypeId);
            return View(Claim);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim Claim = _claims.Get(c => c.Customer.Email == User.Identity.Name && c.ClaimId == id, null, "ClaimPriority,ClaimState,ClaimType,Customer,Employee").FirstOrDefault();
            if (Claim == null)
            {
                return HttpNotFound();
            }
            return View(Claim);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Claim Claim = _claims.Get(c => c.Customer.Email == User.Identity.Name && c.ClaimId == id, null, null).FirstOrDefault(); 
            _claims.Delete(Claim);
            _claims.SaveObjects();
            return RedirectToAction("Index");
        }

    }
}