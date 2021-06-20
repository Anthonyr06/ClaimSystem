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
    public class EmployeesController : Controller
    {

        private readonly RepositoryEF<Employee> _employees;
        private readonly RepositoryEF<Address> _addresses;
        private readonly RepositoryEF<Position> _positions;

        public EmployeesController()
        {
            var dbContext = new Data.AppDbContext();
            _employees = new RepositoryEF<Employee>(dbContext);
            _addresses = new RepositoryEF<Address>(dbContext);
            _positions = new RepositoryEF<Position>(dbContext);
        }

        // GET: Employees
        public ActionResult Index()
        {
            var employees = _employees.Get(null, null, "Address,Position");
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employees.GetByID(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_addresses.Get(), "AddressId", "Neighborhood");
            ViewBag.PositionId = new SelectList(_positions.Get(), "PositionId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cedula,Name,LastName,Email,Password,PhoneNumber,AdmissionDate,PositionId,AddressId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                var check = _employees.Get(e => e.Email == employee.Email.Trim().ToLower());

                if (check == null)
                {
                    employee.Password = MD5Service.GetMD5(employee.Password);
                    _employees.Insert(employee);
                    _employees.SaveObjects();
                    return RedirectToAction("Index");
                }

            }

            ViewBag.EmployeeId = new SelectList(_addresses.Get(), "AddressId", "Neighborhood", employee.EmployeeId);
            ViewBag.PositionId = new SelectList(_positions.Get(), "PositionId", "Name", employee.PositionId);

            ModelState.AddModelError("", "El usuario ya existe");
            return View();
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employees.GetByID(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(_addresses.Get(), "AddressId", "Neighborhood", employee.EmployeeId);
            ViewBag.PositionId = new SelectList(_positions.Get(), "PositionId", "Name", employee.PositionId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Cedula,Name,LastName,Email,Password,PhoneNumber,AdmissionDate,PositionId,AddressId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employees.Update(employee);
                _employees.SaveObjects();

                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_addresses.Get(), "AddressId", "Neighborhood", employee.EmployeeId);
            ViewBag.PositionId = new SelectList(_positions.Get(), "PositionId", "Name", employee.PositionId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = _employees.GetByID(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = _employees.GetByID(id);
            _employees.Delete(employee);
            _employees.SaveObjects();
            return RedirectToAction("Index");
        }
    }
}
