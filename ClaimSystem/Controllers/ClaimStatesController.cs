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
    public class ClaimStatesController : Controller
    {
        private readonly RepositoryEF<ClaimState> _claimStates;

        public ClaimStatesController()
        {
            var dbContext = new AppDbContext();
            _claimStates = new RepositoryEF<ClaimState>(dbContext);
        }

        // GET: ClaimStates
        public ActionResult Index()
        {
            return View(_claimStates.Get());
        }

        // GET: ClaimStates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimState claimState = _claimStates.GetByID(id);
            if (claimState == null)
            {
                return HttpNotFound();
            }
            return View(claimState);
        }

        // GET: ClaimStates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClaimStates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClaimStateId,Desc")] ClaimState claimState)
        {
            if (ModelState.IsValid)
            {
                _claimStates.Insert(claimState);
                _claimStates.SaveObjects();
                return RedirectToAction("Index");
            }

            return View(claimState);
        }

        // GET: ClaimStates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimState claimState = _claimStates.GetByID(id);
            if (claimState == null)
            {
                return HttpNotFound();
            }
            return View(claimState);
        }

        // POST: ClaimStates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClaimState claimState)
        {
            if (ModelState.IsValid)
            {
                ClaimState c = _claimStates.GetByID(claimState.ClaimStateId);
                c.Desc = claimState.Desc;

                _claimStates.Update(c);
                _claimStates.SaveObjects();

                return RedirectToAction("Index");
            }
            return View(claimState);
        }

        // GET: ClaimStates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimState claimState = _claimStates.GetByID(id);
            if (claimState == null)
            {
                return HttpNotFound();
            }
            return View(claimState);
        }

        // POST: ClaimStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClaimState claimState = _claimStates.GetByID(id);
            _claimStates.Delete(claimState);
            _claimStates.SaveObjects();
            return RedirectToAction("Index");
        }
    }
}
