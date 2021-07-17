using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClaimSystem.Models;
using ClaimSystem.Services;
using ClaimSystem.Data;

namespace ClaimSystem.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly RepositoryEF<Claim> _claims;
        private readonly RepositoryEF<ClaimPriority> _claimPriorities;
        private readonly RepositoryEF<ClaimState> _claimsStates;
        private readonly RepositoryEF<ClaimType> _claimsTypes;
        private readonly RepositoryEF<Customer> _customers;
        private readonly RepositoryEF<Employee> _employees;

        public ClaimsController()
        {
            var dbContext = new AppDbContext();
            _claims = new RepositoryEF<Claim>(dbContext);
            _customers = new RepositoryEF<Customer>(dbContext);
            _employees = new RepositoryEF<Employee>(dbContext);
            _claimPriorities = new RepositoryEF<ClaimPriority>(dbContext);
            _claimsStates = new RepositoryEF<ClaimState>(dbContext);
            _claimsTypes = new RepositoryEF<ClaimType>(dbContext);
        }

        // GET: Claims
        public ActionResult Index()
        {
            var claims = _claims.Get(null, null, "ClaimPriority,ClaimState,ClaimType,Customer,Employee");
            return View(claims);
        }

        // GET: Claims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim Claim = _claims.Get(c => c.ClaimId == id, null, "ClaimPriority,ClaimState,ClaimType,Customer,Employee").FirstOrDefault();
            if (Claim == null)
            {
                return HttpNotFound();
            }
            return View(Claim);
        }

        // GET: Claims/Create
        public ActionResult Create()
        {
            ViewBag.ClaimPriorityId = new SelectList(_claimPriorities.Get(), "ClaimPriorityId", "Desc");
            ViewBag.ClaimStateId = new SelectList(_claimsStates.Get(), "ClaimStateId", "Desc");
            ViewBag.ClaimTypeId = new SelectList(_claimsTypes.Get(), "ClaimTypeId", "Desc");
            ViewBag.CustomerId = new SelectList(_customers.Get(), "CustomerId", "Cedula");
            ViewBag.EmployeeId = new SelectList(_employees.Get(), "EmployeeId", "Cedula");
            return View();
        }

        // POST: Claims/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Desc,StartDate,SolutionDate,Solution,ClaimTypeId,ClaimStateId,ClaimPriorityId,EmployeeId,CustomerId")] Claim Claim)
        {
            if (ModelState.IsValid)
            {
                _claims.Insert(Claim);
                _claims.SaveObjects();
                return RedirectToAction("Index");
            }

            ViewBag.ClaimPriorityId = new SelectList(_claimPriorities.Get(), "ClaimPriorityId", "Desc", Claim.ClaimPriorityId);
            ViewBag.ClaimStateId = new SelectList(_claimsStates.Get(), "ClaimStateId", "Desc", Claim.ClaimStateId);
            ViewBag.ClaimTypeId = new SelectList(_claimsTypes.Get(), "ClaimTypeId", "Desc", Claim.ClaimTypeId);
            ViewBag.CustomerId = new SelectList(_customers.Get(), "CustomerId", "Cedula", Claim.CustomerId);
            ViewBag.EmployeeId = new SelectList(_employees.Get(), "EmployeeId", "Cedula", Claim.EmployeeId);
            return View(Claim);
        }

        // GET: Claims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim Claim = _claims.GetByID(id);
            if (Claim == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClaimPriorityId = new SelectList(_claimPriorities.Get(), "ClaimPriorityId", "Desc", Claim.ClaimPriorityId);
            ViewBag.ClaimStateId = new SelectList(_claimsStates.Get(), "ClaimStateId", "Desc", Claim.ClaimStateId);
            ViewBag.ClaimTypeId = new SelectList(_claimsTypes.Get(), "ClaimTypeId", "Desc", Claim.ClaimTypeId);
            ViewBag.CustomerId = new SelectList(_customers.Get(), "CustomerId", "Cedula", Claim.CustomerId);
            ViewBag.EmployeeId = new SelectList(_employees.Get(), "EmployeeId", "Cedula", Claim.EmployeeId);
            return View(Claim);
        }

        // POST: Claims/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Claim Claim)
        {
            if (ModelState.IsValid)
            {
                Claim c = _claims.GetByID(Claim.ClaimId);
                c.Desc = Claim.Desc;
                c.StartDate = Claim.StartDate;
                c.SolutionDate = Claim.SolutionDate;
                c.Solution = Claim.Solution;
                c.ClaimTypeId = Claim.ClaimTypeId;
                c.ClaimStateId = Claim.ClaimStateId;
                c.ClaimPriorityId = Claim.ClaimPriorityId;
                c.CustomerId = Claim.CustomerId;
                c.EmployeeId = Claim.EmployeeId;

                _claims.Update(c);
                _claims.SaveObjects();
                return RedirectToAction("Index");
            }
            ViewBag.ClaimPriorityId = new SelectList(_claimPriorities.Get(), "ClaimPriorityId", "Desc", Claim.ClaimPriorityId);
            ViewBag.ClaimStateId = new SelectList(_claimsStates.Get(), "ClaimStateId", "Desc", Claim.ClaimStateId);
            ViewBag.ClaimTypeId = new SelectList(_claimsTypes.Get(), "ClaimTypeId", "Desc", Claim.ClaimTypeId);
            ViewBag.CustomerId = new SelectList(_customers.Get(), "CustomerId", "Cedula", Claim.CustomerId);
            ViewBag.EmployeeId = new SelectList(_employees.Get(), "EmployeeId", "Cedula", Claim.EmployeeId);
            return View(Claim);
        }

        // GET: Claims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Claim Claim = _claims.Get(c => c.ClaimId == id, null, "ClaimPriority,ClaimState,ClaimType,Customer,Employee").FirstOrDefault();
            if (Claim == null)
            {
                return HttpNotFound();
            }
            return View(Claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Claim Claim = _claims.GetByID(id);
            _claims.Delete(Claim);
            _claims.SaveObjects();
            return RedirectToAction("Index");
        }
    }
}
