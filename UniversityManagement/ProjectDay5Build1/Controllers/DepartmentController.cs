using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectDay5Build1.Models;

namespace ProjectDay5Build1.Controllers
{
    public class DepartmentController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Department/

        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

       //
        // GET: /Department/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Department/Create

        [HttpPost]
        public ActionResult Create(Department department)
        {
            department.DepartmentCode = department.DepartmentCode.ToUpper();
            department.DepartmentName = department.DepartmentName.ToUpper();
            
            // check validation 
            string message = ValidationForDepartment(department);
            if (message != "")
            {
                ViewBag.Message = message;
                return View(department);
            }

            // save it to the database
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(department);
        }

        private string ValidationForDepartment(Department department)
        {
            string message = "";
            if (department.DepartmentCode == "")
            {
                message += "Code not given";
            }
            if (department.DepartmentCode != "" && db.Departments.Count(d => d.DepartmentCode == department.DepartmentCode) != 0)
            {
                message += "Code already exists" + "\n";
            }
            if (department.DepartmentName == "")
            {
                message += "Name not given";
            }
            if (department.DepartmentName != "" && db.Departments.Count(d => d.DepartmentName == department.DepartmentName) != 0)
            {
                message += "Name already exists" + "\n";
            }
            return message;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult IsCodeValid(string code)
        {
            bool result = db.Departments.Count(d => d.DepartmentCode == code) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNameValid(string name)
        {
            bool result = db.Departments.Count(d => d.DepartmentName == name) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}