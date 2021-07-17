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
    public class PositionsController : Controller
    {
        private readonly RepositoryEF<Position> _positions;
        private readonly RepositoryEF<Department> _departments;

        public PositionsController()
        {
            var dbContext = new AppDbContext();
            _positions = new RepositoryEF<Position>(dbContext);
            _departments = new RepositoryEF<Department>(dbContext);
        }

        // GET: Positions
        public ActionResult Index()
        {
            var positions = _positions.Get(null, null, nameof(Position.Department));
            return View(positions.ToList());
        }

        // GET: Positions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = _positions.Get(d => d.PositionId == id, null, nameof(Position.Department)).FirstOrDefault();
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // GET: Positions/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(_departments.Get(), nameof(Department.DepartmentId), nameof(Department.Room));
            return View();
        }

        // POST: Positions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PositionId,Name,Desc,Salary,Rank,DepartmentId")] Position position)
        {
            if (ModelState.IsValid)
            {
                _positions.Insert(position);
                _positions.SaveObjects();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(_departments.Get(), nameof(Department.DepartmentId), nameof(Department.Room), position.DepartmentId);
            return View(position);
        }

        // GET: Positions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = _positions.GetByID(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(_departments.Get(), nameof(Department.DepartmentId), nameof(Department.Room), position.DepartmentId);
            return View(position);
        }

        // POST: Positions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Position position)
        {
            if (ModelState.IsValid)
            {
                Position p = _positions.GetByID(position.PositionId);
                p.Name = position.Name;
                p.Desc = position.Desc;
                p.Salary = position.Salary;
                p.Rank = position.Rank;
                p.DepartmentId = position.DepartmentId;

                _positions.Update(p);
                _positions.SaveObjects();

                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(_departments.Get(), nameof(Department.DepartmentId), nameof(Department.Room), position.DepartmentId);
            return View(position);
        }

        // GET: Positions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = _positions.Get(d => d.PositionId == id, null, nameof(Position.Department)).FirstOrDefault();
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: Positions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Position position = _positions.GetByID(id);
            _positions.Delete(position);
            _positions.SaveObjects();
            return RedirectToAction("Index");
        }

    }
}
