using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAPP1.Models;

namespace WebAPP1.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeContext obj = new EmployeeContext();
        // GET: Employee

        public ActionResult Index()
        {
            var empdata = obj.GetEmployees();
            return View(empdata);
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            ViewBag.Message = string.Format("Record Deleted Successfully");
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            var empdata = obj.GetEmployees();
            return View(empdata);
        }
        [HttpPost]
        public ActionResult Add(Employee emp)
        {
            int empdata = obj.CreateEmployee(emp);
            if (empdata == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();

            }
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            Employee empdata = obj.GetEmployeeByID(id);

            return View(empdata);

        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            int empdata = obj.UpdateEmployee(emp);


            if (empdata == 1)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();

            }

        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Employee empdata = obj.GetEmployeeByID(id);

            return View(empdata);


        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(string id)
        {
            int empdata = obj.DeleteEmployee(id);
            if (empdata == 1)
            {
                TempData["Message"] = "Test";
                return RedirectToAction("Index");

            }
            else
            {
                return View();
            }
        }
    }
}