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
    public class CourseController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Course/

        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Department).Include(c => c.Semester);
            return View(courses.ToList());
        }

        //
        // GET: /Course/Create

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode");
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName");
            return View();
        }

        //
        // POST: /Course/Create

        [HttpPost]
        public ActionResult Create(Course course)
        {
            //check validation 
            string message = ValidationForCourse(course); 
            if (message != "")
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", course.DepartmentId);
                ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", course.SemesterId);
                ViewBag.Message = message;
                return View(course);
            }

            //save to database
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentCode", course.DepartmentId);
            ViewBag.SemesterId = new SelectList(db.Semesters, "SemesterId", "SemesterName", course.SemesterId);
            return View(course);
        }

        private string ValidationForCourse(Course course)
        {
            string message = "";
            if (course.CourseCode == "")
            {
                message += "Code not given";
            }

            if (course.CourseCode != "" && db.Courses.Count(c => c.CourseCode == course.CourseCode) != 0)
            {
                message += "Code already exists ";
            }
            if (course.CourseCredit < 0)
            {
                message += "Credit must be non-negative number";
            }
            if (course.CourseName == "")
            {
                message += "Name not given";
            }
            if (course.CourseName != "" && db.Courses.Count(c => c.CourseName == course.CourseName) != 0)
            {
                message += "Name already exists";
            }

            if (course.CourseDescription == "")
            {
                message += "Description not given";
            }

            if (course.DepartmentId < 1)
            {
                message += "Invalid Department Selected";
            }

            if (course.SemesterId < 1)
            {
                message += "Invalid Semester Selected";
            }

            return message;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult CourseInfo(int? departmentId)
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return
                View(
                    db.Courses
                      .Include(s => s.Department)
                        .Where(s => s.Department.DepartmentId == departmentId)
                      .Include(s => s.Semester)
                      .Include(s => s.Teacher)
                    );

        }

        public ActionResult CourseAssign()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.TeacherId = new SelectList("", "", "TeacherName");
            ViewBag.CourseId = new SelectList("", "", "CourseName");

            return View();
        }

        [HttpPost]
        public ActionResult CourseAssign(Department aDepartment, Course aCourse, Teacher aTeacher)
        {
            string message = ValidationForCourseAssign(aDepartment, aCourse, aTeacher);
            if (message != "")
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                ViewBag.TeacherId = new SelectList("", "", "TeacherName");
                ViewBag.CourseId = new SelectList("", "", "CourseName");

                ViewBag.Message = message;
                return View();
            }

            aTeacher = db.Teachers.Find(aTeacher.TeacherId);
            aCourse = db.Courses.Find(aCourse.CourseId);
            
            // assiging teacher to this course 
            aCourse.Teacher = aTeacher; 

            // update remaining credit for teacher 
            aTeacher.TeacherRemainingCredit -= aCourse.CourseCredit;

            //save data to database 
            if (!ModelState.IsValid)
            {
                db.Entry(aCourse).State = EntityState.Modified;
                db.SaveChanges();

                db.Entry(aTeacher).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View();
        }

        private string ValidationForCourseAssign(Department aDepartment, Course aCourse, Teacher aTeacher)
        {
            string message = "";
            
            if (aDepartment.DepartmentId == 0 || aCourse.CourseId == 0 || aTeacher.TeacherId == 0)
            {
                message += "Incomplete form submit, any field is not yet set ";
            }

            // is teacher and course is not from the same department

            aDepartment = db.Departments.Find(aDepartment.DepartmentId);
            aTeacher = db.Teachers.Find(aTeacher.TeacherId);
            aCourse = db.Courses.Find(aCourse.CourseId);


            if (aDepartment.DepartmentId != aTeacher.DepartmentId || aCourse.DepartmentId != aTeacher.DepartmentId)
            {
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName");
                ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");

                
                if (aDepartment.DepartmentId != aCourse.DepartmentId)
                {
                    message += "Your Course is not from the selected department";

                }
                else if (aCourse.DepartmentId != aTeacher.DepartmentId)
                {
                    message += "Your Teacher is not from the selected department";
                }

            }

            // if course is already assigned 

            if (aCourse.Teacher != null)
            {
                message += "Course Already Assigned";
            }

            return message; 
        }

        public JsonResult IsCodeValid(string code)
        {
            var result = db.Courses.Count(c => c.CourseCode == code) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNameValid(string name)
        {
            var result = db.Courses.Count(c => c.CourseName == name) == 0;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectTeacherFromDepartment(int? departmentId)
        {
            var teachers = db.Teachers.Where(t => t.DepartmentId == departmentId);
            ViewBag.TeacherId = new SelectList(teachers.ToArray(), "TeacherId", "TeacherName");
            return PartialView("_Teacher", ViewData["TeacherId"]);
        }

        public ActionResult SelectCourseFromDepartment(int? departmentId)
        {
            var courses = db.Courses.Where(c => c.DepartmentId == departmentId);
            ViewBag.CourseId = new SelectList(courses.ToArray(), "CourseId", "CourseName");
            return PartialView("_Course", ViewData["CourseId"]);
        }

        public ActionResult GetTeacherDetails(int? teacherId)
        {
            Teacher teacher = db.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
            return PartialView("_TeacherDetails", teacher);
        }

        public ActionResult GetCourseDetails(int? courseId)
        {
            Course course = db.Courses.FirstOrDefault(c => c.CourseId == courseId);
            return PartialView("_CourseDetails",course);
        }
    }
}