using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectDay5Build1.Models;
using RazorPDF;
using iTextSharp;
using iTextSharp.text;
namespace ProjectDay5Build1.Controllers
{
    public class EnrollmentController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //
        // GET: /Enrollment/

        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Student).Include(e => e.Course);
            return View(enrollments.ToList());
        }

        //
        // GET: /Enrollment/Create

        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNo");
            ViewBag.CourseId = new SelectList(db.Courses, "", "");
            return View();
        }

        //
        // POST: /Enrollment/Create

        [HttpPost]
        public ActionResult Create(Enrollment enrollment, Course aCourse)
        {
            // validation check 
            string message = ValidationForEnrollment(enrollment, aCourse); 
            if (message != "")
            {
                ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNo");
                ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
                ViewBag.Message = message;

                return View();
            }

           
            // assign course & student to the Enrollment  
            aCourse = (from c in db.Courses where c.CourseId == aCourse.CourseId select c).SingleOrDefault();
            Student aStudent =
                (from s in db.Students where s.StudentId == enrollment.StudentId select s).SingleOrDefault();
            
            enrollment.Course = aCourse;
            enrollment.Student = aStudent;
           // enrollment.Course.CourseId = aCourse.CourseId; 

            if (!ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNo", enrollment.StudentId);
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", enrollment.Course.CourseId);

            return View(); 
        }

        public ActionResult SelectCoursesOfDepartment(int? studentId)
        {
            try
            {
                Student student = (from s in db.Students where s.StudentId == studentId select s).FirstOrDefault();
                var courses = (from e in db.Courses where e.DepartmentId == student.DepartmentId select e).ToList();
                ViewBag.CourseId = new SelectList(courses.ToArray(), "CourseId", "CourseName");
                return PartialView("_Course", ViewData["CourseId"]);
            }
            catch (Exception)
            {
                return PartialView("_Course");
            }
        }

        private string ValidationForEnrollment(Enrollment enrollment, Course aCourse)
        {
            string message = "";
            if (aCourse == null || aCourse.CourseId == 0 || enrollment.StudentId == 0 || enrollment.StudentId == null)
            {
                message += "Please fill up all the options";
            }
            else
            {

                // what about student not found, course not found 
                // case not considered due to fake trust 

                aCourse = (from c in db.Courses where c.CourseId == aCourse.CourseId select c).SingleOrDefault();
                List<Enrollment> enrollments =
                    (from e in db.Enrollments where e.StudentId == enrollment.StudentId select e).ToList();

                //check student is already enrolled in this course or not
                bool alreadyEnrollerd = false;
                foreach (Enrollment e in enrollments.Where(e => e.Course.CourseId == aCourse.CourseId))
                {
                    alreadyEnrollerd = true;
                }

                if (alreadyEnrollerd)
                {
                    message += "Student Already Enrolled in this Course";
                }
            }
            return message; 
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        
        public ViewResult ViewResult(int? studentId)
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNo");
            return
                View(
                    db.Enrollments.Include(s => s.Student)
                      .Where(s => s.Student.StudentId == studentId)
                      .Include(s => s.Student.Department)
                      .Include(s => s.Course)
                      .Include(s => s.Grade)
                    );
        }

        [HttpPost]
        public ActionResult ViewPdfResult(int? studentId)
        {
            
            var pdfResult = new PdfResult(
                        db.Enrollments.Include(s => s.Student)
                        .Where(s => s.Student.StudentId == studentId)
                        .Include(s => s.Student.Department)
                        .Include(s => s.Course)
                        .Include(s => s.Grade), 
                        "ViewPdfResult"
                        );
            
            return pdfResult;
        }
        public ActionResult ResultEntry()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNo");
            ViewBag.CourseId = new SelectList("", "", "CourseName");
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeCode");
            return View(); 
        }

        [HttpPost]
        public ActionResult ResultEntry(Enrollment enrollment, Course aCourse)
        {
            string message = ValidationForResultEntry(enrollment, aCourse); 
            if (message != "")
            {
                ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNo");
                ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
                ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeCode");

                ViewBag.Message = message;
                return View();
            }

           
            Grade aGrade = (from g in db.Grades where g.GradeId == enrollment.GradeId select g).SingleOrDefault();
            Student aStudent = (from s in db.Students where s.StudentId == enrollment.StudentId select s).SingleOrDefault();
            aCourse = (from c in db.Courses where c.CourseId == aCourse.CourseId select c).SingleOrDefault();

            Enrollment aEnrollment =
                (from e in db.Enrollments
                 where e.StudentId == aStudent.StudentId && e.Course.CourseId == aCourse.CourseId
                 select e).SingleOrDefault();
            
            enrollment = aEnrollment;
            enrollment.Grade = aGrade;
            enrollment.GradeId = aGrade.GradeId;
            enrollment.Student = aStudent;
            enrollment.StudentId = aStudent.StudentId;
            enrollment.Course = aCourse; 

            if (!ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentRegistrationNo");
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewBag.GradeId = new SelectList(db.Grades, "GradeId", "GradeCode");

            return View();

        }

        private string ValidationForResultEntry(Enrollment enrollment, Course aCourse)
        {
            string message = "";
            if (aCourse.CourseId < 1 || enrollment.StudentId < 1 || enrollment.GradeId < 1 || aCourse == null || enrollment.GradeId == null || enrollment.StudentId == null)
            {
                message += "Fill Up all fields.";
                return message;
            }
            else
            {
                try
                {
                    Student aStudent = (from s in db.Students where s.StudentId == enrollment.StudentId select s).SingleOrDefault();
                    aCourse = (from c in db.Courses where c.CourseId == aCourse.CourseId select c).SingleOrDefault();

                    Enrollment aEnrollment =
                        (from e in db.Enrollments
                         where e.StudentId == aStudent.StudentId && e.Course.CourseId == aCourse.CourseId
                         select e).SingleOrDefault();
                    if (aEnrollment == null)
                    {
                        message += "Student is not enrolled in this course.";
                    }
                    else
                    {
                        if (aEnrollment.GradeId > 0)
                        {
                            message = "Grade is already added to this course";
                        }    
                    }
                }
                catch (Exception)
                {
                    message += " ";
                }

            }
            return message;
        }

        public ActionResult SelectCoursesOfStudent(int? studentId)
        {
            var courses = (from e in db.Enrollments where e.StudentId == studentId select e.Course).ToList();
            ViewBag.CourseId = new SelectList(courses.ToArray(), "CourseId", "CourseName");
            return PartialView("_Course", ViewData["CourseId"]);
        }

        public ActionResult GetStudentDetails(int? studentId)
        {
            try
            {
                if (studentId == null)
                {
                    studentId = 0;
                }
                Student student = db.Students.FirstOrDefault(s => s.StudentId == studentId);
                Department department = db.Departments.FirstOrDefault(d => d.DepartmentId == student.DepartmentId);

                student.Department = department;

                return PartialView("_StudentDetails", student);
            }
            catch (Exception)
            {
                return PartialView("_StudentDetails");
            }
        }

    }
}