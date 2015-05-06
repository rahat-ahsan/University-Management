using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ProjectDay5Build1.Models;

namespace ProjectDay5Build1.Controllers
{
    public class TeacherController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        
        public ActionResult Index()
        {
            var teachers = db.Teachers.Include(t => t.Designation).Include(t => t.Department);
            return View(teachers.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            string message = ValidationForTeacher(teacher);
            if (message != "")
            {
                ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", teacher.DepartmentId);
                ViewBag.Message = message; 
                return View(teacher);
            }

            teacher.TeacherRemainingCredit = teacher.TeacherCreditToBeTaken;
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacher.DesignationId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", teacher.DepartmentId);
            return View(teacher);
        }

        private string ValidationForTeacher(Teacher teacher)
        {
            string message = ""; 

            if (teacher.TeacherName == "")
            {
                message += "Teacher Name is not given";
            }

            if (teacher.TeacherAddress == "")
            {
                message += "Teacher Address is not given";
            }

            if (teacher.TeacherEmail == "")
            {
                message += "Teacher email is not given";
            }
            else
            {
                if (!IsMailAddressValid(teacher.TeacherEmail))
                {
                    message += "Invalid email address given";
                }
                else
                {   
                    // there is case, story says email must be unique 
                    // but it is not said that unique within the system or not 
                   
                    if (db.Teachers.Count(t => t.TeacherEmail == teacher.TeacherEmail) != 0)
                    {
                        message += "This email is already exists";
                    }
                }    
            }
            
            if (teacher.TeacherContactNo == "")
            {
                message += "Contact No. not given";
            }

            if (teacher.DesignationId < 0)
            {
                message += "Invalid Designation, choose correct designation";
            }

            if (teacher.DepartmentId < 0)
            {
                message += "Invalid Department, choose correct department";
            }

            if (teacher.TeacherCreditToBeTaken < 0)
            {
                message += "Credit to be taken must be non-negative number";
            }

            return message;
        }

        private bool IsMailAddressValid(string teacherEmail)
        {
            string strRegex =   @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            
            if (re.IsMatch(teacherEmail))
                return (true);
            
            return (false);
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}