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
    public class ClaimTypesController : Controller
    {
        private readonly RepositoryEF<ClaimType> _claimTypes;

        public ClaimTypesController()
        {
            var dbContext = new AppDbContext();
            _claimTypes = new RepositoryEF<ClaimType>(dbContext);
        }

        // GET: ClaimTypes
        public ActionResult Index()
        {
            return View(_claimTypes.Get());
        }

        // GET: ClaimTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimType claimType = _claimTypes.GetByID(id);
            if (claimType == null)
            {
                return HttpNotFound();
            }
            return View(claimType);
        }

        // GET: ClaimTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClaimTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClaimTypeId,Desc")] ClaimType claimType)
        {
            if (ModelState.IsValid)
            {
                _claimTypes.Insert(claimType);
                _claimTypes.SaveObjects();
                return RedirectToAction("Index");
            }

            return View(claimType);
        }

        // GET: ClaimTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimType claimType = _claimTypes.GetByID(id);
            if (claimType == null)
            {
                return HttpNotFound();
            }
            return View(claimType);
        }

        // POST: ClaimTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClaimTypeId,Desc")] ClaimType claimType)
        {
            if (ModelState.IsValid)
            {
                ClaimType c = _claimTypes.GetByID(claimType.ClaimTypeId);
                c.Desc = claimType.Desc;

                _claimTypes.Update(c);
                _claimTypes.SaveObjects();

                return RedirectToAction("Index");
            }
            return View(claimType);
        }

        // GET: ClaimTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClaimType claimType = _claimTypes.GetByID(id);
            if (claimType == null)
            {
                return HttpNotFound();
            }
            return View(claimType);
        }

        // POST: ClaimTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClaimType claimType = _claimTypes.GetByID(id);
            _claimTypes.Delete(claimType);
            _claimTypes.SaveObjects();
            return RedirectToAction("Index");
        }

    }
}
