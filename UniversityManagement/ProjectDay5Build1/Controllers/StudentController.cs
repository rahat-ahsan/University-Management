using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ProjectDay5Build1.Models;

namespace ProjectDay5Build1.Controllers
{
    public class StudentController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Department);
            return View(students.ToList());
        }

        public ActionResult Details(int id = 0)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            //check validation
            string message = ValidationForStudent(student); 
            if (message != "")
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", student.DepartmentId);
                ViewBag.Message = message; 
                return View(student);
            }

            // Get the Registration No
            student.StudentRegistrationNo = GetStudentRegistrationNo(student); 

            //save in the database
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", student.DepartmentId);
            return View(student);
        }

        private string GetStudentRegistrationNo(Student student)
        {
            //List<Student> students = (
            //    from s in db.Students
            //    where s.DepartmentId == student.DepartmentId && DateTime.Now.Year == s.StudentRegistrationDate.Year
            //    select s).ToList();

            List<Student> students = (
               from s in db.Students
               where s.DepartmentId == student.DepartmentId 
               select s).ToList();

            List<Student> studentsList = students.Where(StudentOfThisYear).ToList();
            students = studentsList;

            Department department =
                 (from d in db.Departments
                  where d.DepartmentId == student.DepartmentId
                  select d).Single();

            string regNo = department.DepartmentCode + "-" +
                           (DateTime.Now.Year).ToString() + "-";

            int count = students.Count + 1;

            if (count < 10) regNo += "00";
            else if (count < 100) regNo += "0";

            regNo += count.ToString();
            
            return regNo;
        }

        private bool StudentOfThisYear(Student aStudent)
        {
           string aYear = DateTime.Now.Year.ToString();
           string bYear = aStudent.StudentRegistrationNo;
           int firstDashPosition = bYear.IndexOf('-') +1;
           bYear = bYear.Substring(firstDashPosition, 4);
           
            return aYear == bYear;
        }

        private string ValidationForStudent(Student student)
        {
            string message = ""; 

            if (student.StudentName == "")
            {
                message += "Name is not given";
            }

            if (student.StudentEmail == "")
            {
                message += "Email address is not given";
            }
            else
            {
                if (!IsValidEmailId(student.StudentEmail))
                {
                    message += "Invalid Email Address given";
                }
                else
                {
                    if (db.Students.Count(s => s.StudentEmail == student.StudentEmail) != 0)
                    {
                        message += "Email already exists";
                    }
                }
            }

            if (student.StudentContactNo == "")
            {
                message += "Contact no not given";
            }

            if (student.StudentAddress == "")
            {
                message += "Address not given";
            }

            if (student.DepartmentId < 0)
            {
                message += "Invalid Department Selected";
            }

            return message;
        }

        private bool IsValidEmailId(string studentEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);

            if (re.IsMatch(studentEmail))
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