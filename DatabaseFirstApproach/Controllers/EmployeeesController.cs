using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseFirstApproach.Models;

namespace DatabaseFirstApproach.Controllers
{
    public class EmployeeesController : Controller
    {
        private EmployeeEntities db = new EmployeeEntities();

        // GET: Employeees
        public ActionResult Index()
        {
            return View(db.Employeees.ToList());
        }

        // GET: Employeees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employeee employeee = db.Employeees.Find(id);
            if (employeee == null)
            {
                return HttpNotFound();
            }
            return View(employeee);
        }

        // GET: Employeees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employeees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpID,EmpName,EmpSalary")] Employeee employeee)
        {
            if (ModelState.IsValid)
            {
                db.Employeees.Add(employeee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeee);
        }

        // GET: Employeees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employeee employeee = db.Employeees.Find(id);
            if (employeee == null)
            {
                return HttpNotFound();
            }
            return View(employeee);
        }

        // POST: Employeees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpID,EmpName,EmpSalary")] Employeee employeee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeee);
        }

        // GET: Employeees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employeee employeee = db.Employeees.Find(id);
            if (employeee == null)
            {
                return HttpNotFound();
            }
            return View(employeee);
        }

        // POST: Employeees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employeee employeee = db.Employeees.Find(id);
            db.Employeees.Remove(employeee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
