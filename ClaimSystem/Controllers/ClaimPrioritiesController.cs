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
    public class ClaimPrioritiesController : Controller
    {
        private readonly RepositoryEF<ClaimPriority> _claimPriorities;

        public ClaimPrioritiesController()
        {
            var dbContext = new AppDbContext();
            _claimPriorities = new RepositoryEF<ClaimPriority>(dbContext);
        }

        // GET: ClaimPriorities
        public ActionResult Index()
        {
            return View(_claimPriorities.Get());
        }

        // GET: ClaimPriorities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimPriority claimPriority = _claimPriorities.GetByID(id);
            if (claimPriority == null)
            {
                return HttpNotFound();
            }
            return View(claimPriority);
        }

        // GET: ClaimPriorities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClaimPriorities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClaimPriorityId,Desc")] ClaimPriority claimPriority)
        {
            if (ModelState.IsValid)
            {
                _claimPriorities.Insert(claimPriority);
                _claimPriorities.SaveObjects();
                return RedirectToAction("Index");
            }

            return View(claimPriority);
        }

        // GET: ClaimPriorities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimPriority claimPriority = _claimPriorities.GetByID(id);
            if (claimPriority == null)
            {
                return HttpNotFound();
            }
            return View(claimPriority);
        }

        // POST: ClaimPriorities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClaimPriority claimPriority)
        {
            if (ModelState.IsValid)
            {
                ClaimPriority c = _claimPriorities.GetByID(claimPriority.ClaimPriorityId);
                c.Desc = claimPriority.Desc;

                _claimPriorities.Update(c);
                _claimPriorities.SaveObjects();

                return RedirectToAction("Index");
            }
            return View(claimPriority);
        }

        // GET: ClaimPriorities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimPriority claimPriority = _claimPriorities.GetByID(id);
            if (claimPriority == null)
            {
                return HttpNotFound();
            }
            return View(claimPriority);
        }

        // POST: ClaimPriorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClaimPriority claimPriority = _claimPriorities.GetByID(id);
            _claimPriorities.Delete(claimPriority);
            _claimPriorities.SaveObjects();
            return RedirectToAction("Index");
        }
    }
}
