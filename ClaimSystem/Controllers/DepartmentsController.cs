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
    public class DepartmentsController : Controller
    {
        private readonly RepositoryEF<Department> _departments;

        public DepartmentsController()
        {
            var dbContext = new AppDbContext();
            _departments = new RepositoryEF<Department>(dbContext);
        }

        // GET: Departments
        public ActionResult Index()
        {
            return View(_departments.Get());
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departments.GetByID(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,Name,Desc")] Department department)
        {
            if (ModelState.IsValid)
            {
                _departments.Insert(department);
                _departments.SaveObjects();
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departments.GetByID(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                Department d = _departments.GetByID(department.DepartmentId);
                d.Name = department.Name;
                d.Desc = department.Desc;

                _departments.Update(d);
                _departments.SaveObjects();

                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = _departments.GetByID(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = _departments.GetByID(id);
            _departments.Delete(department);
            _departments.SaveObjects();
            return RedirectToAction("Index");
        }
    }
}
