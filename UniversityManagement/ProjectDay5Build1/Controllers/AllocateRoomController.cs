using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectDay5Build1.Models;

namespace ProjectDay5Build1.Controllers
{
    public class AllocateRoomController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var allocaterooms = db.AllocateRooms.Include(a => a.Course).Include(a => a.Room).Include(a => a.Day);
            return View(allocaterooms.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo");
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(AllocateRoom allocateroom)
        {
            // Validation Check 
            string message = ValidationForAllocateRoom(allocateroom);
            
            if (message != "")
            {
                ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", allocateroom.CourseId);
                ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", allocateroom.RoomId);
                ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", allocateroom.DayId);

                ViewBag.ErrorMessage = "Validation Error: " + message;

                return View(allocateroom);    
            }

            // conflict

            message = GetAllRoomAllocationConflict(allocateroom); 

            if (message != "")
            {
                ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", allocateroom.CourseId);
                ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", allocateroom.RoomId);
                ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", allocateroom.DayId);

                ViewBag.OverlappingMessage = message;

                return View(allocateroom);
            }


            // get the department 
            Course aCourse =
               (from cor in db.Courses where cor.CourseId == allocateroom.CourseId select cor).Single();
            
            Department aDepartment =
                (from dep in db.Departments where dep.DepartmentId == aCourse.DepartmentId select dep).Single();

            // now assign the department 
            allocateroom.Department = aDepartment;

            if (ModelState.IsValid)
            {
                db.AllocateRooms.Add(allocateroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", allocateroom.CourseId);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomId", "RoomNo", allocateroom.RoomId);
            ViewBag.DayId = new SelectList(db.Days, "DayId", "DayName", allocateroom.DayId);
            
            return View(allocateroom);
        }

        private string GetAllRoomAllocationConflict(AllocateRoom allocateroom)
        {
            string message = "";
            // time conflict 
            string messageTimeConflict = GetTimeConflict(allocateroom);
            if (messageTimeConflict != "")
            {
                messageTimeConflict = "Room is busy. Details: " + messageTimeConflict; 
            }
            
            // this course class is running in another room 
            //string messageCourseConflict = GetCourseConflict(allocateroom); 
            //if (messageCourseConflict != "")
            //{
            //   messageCourseConflict = " Course has class in other room. Details: " + messageCourseConflict; 
            //}

            // This teacher is busy with other class 
            //string messageTeacherConflict = GetTeacherConflict(allocateroom); 
            //if (messageTeacherConflict != "")
            //{
            //    messageTeacherConflict = "This teacher is busy with other class. Details: " + messageTeacherConflict;
            //}
            message = messageTimeConflict;// +messageCourseConflict + messageTeacherConflict; 

            return message; 
        }

        //private string GetTeacherConflict(AllocateRoom allocateroom)
        //{
        //    string message = "";
        //    Course aCourse = db.Courses.Find(allocateroom.CourseId);

        //    if (aCourse.Teacher != null)
        //    {
        //        List<Course> courses =
        //            (from c in db.Courses where c.Teacher.TeacherId == aCourse.Teacher.TeacherId select c).ToList();

        //        // get the enrollment list 
        //        List<AllocateRoom> allocations = (
        //            from course in courses 
        //            from a in db.AllocateRooms
        //            where a.CourseId == course.CourseId && a.DayId == allocateroom.DayId 
        //            select a).ToList();

        //        message = allocations.Where(allocate => IsOverlapped(allocateroom, allocate)).Aggregate(message, (current, allocate) => current + GetOverlappingDetails(allocate));
        //    }
        //    return message; 
        //}

        //private string GetCourseConflict(AllocateRoom allocateroom)
        //{
        //    string message = "";

        //    List<Course> courses =
        //            (from c in db.Courses where c.CourseId == allocateroom.CourseId select c).ToList();
            
        //    // get the enrollment list 
        //    List<AllocateRoom> allocations = (
        //        from course in courses
        //        from a in db.AllocateRooms
        //        where a.CourseId == course.CourseId && a.DayId == allocateroom.DayId
        //        select a).ToList();
            
        //    message = allocations.Where(allocate => IsOverlapped(allocateroom, allocate)).Aggregate(message, (current, allocate) => current + GetOverlappingDetails(allocate));
            
        //    return message;
        //}

        private string GetTimeConflict(AllocateRoom allocateroom)
        {
            string message = "";
            
            List<AllocateRoom> allocations =
                (from a in db.AllocateRooms
                 where (a.DayId == allocateroom.DayId && a.RoomId == allocateroom.RoomId)
                 select a).ToList();

            
            foreach (AllocateRoom allocate in allocations)
            {
                if (IsOverlapped(allocateroom, allocate))
                {
                    message += GetOverlappingDetails(allocate);
                }
            }

            return message; 
        }

        private string ValidationForAllocateRoom(AllocateRoom allocateroom)
        {
            try
            {
                string message = "";


                if (allocateroom.CourseId < 1 || allocateroom.CourseId == null || allocateroom.DayId < 1 ||
                    allocateroom.DayId == null || allocateroom.RoomId < 1 || allocateroom.RoomId == null)
                {
                    message += "All field must be filled up";
                    return message;
                }

                if (!ValidationOfTimeString(allocateroom.StartingTime))
                {
                    message += "Invalid Starting time";
                    return message;
                }
                if (!ValidationOfTimeString(allocateroom.EndingTime))
                {
                    message += "Invalid Ending Time";
                    return message;
                }

                if (ConvertStringToTime(allocateroom.StartingTime) >= ConvertStringToTime(allocateroom.EndingTime))
                {
                    message += "Starting Time is not smaller than ending time";
                }

                return message;
            }
            catch (Exception)
            {
                return " ";
            }
        }

        private string GetOverlappingDetails(AllocateRoom oldAllocation)
        {
            string message = "";
            Course course = (from c in db.Courses where c.CourseId == oldAllocation.CourseId select c).SingleOrDefault();

            if (course != null)
            {
                if( course.Teacher != null)
                    message += "Teacher: " +  course.Teacher.TeacherName + " ";
            }
            message += "Course: " + oldAllocation.Course.CourseName+ " ";
            message += "Staring Time: " + oldAllocation.StartingTime + " ";
            message += "Ending Time: " + oldAllocation.EndingTime + " ;";

            return message; 
        }

        private bool IsOverlapped(AllocateRoom newAllocatation, AllocateRoom oldAllocation)
        {
            int s1 = ConvertStringToTime(newAllocatation.StartingTime);
            int e1 = ConvertStringToTime(newAllocatation.EndingTime);

            int s2 = ConvertStringToTime(oldAllocation.StartingTime);
            int e2 = ConvertStringToTime(oldAllocation.EndingTime);

            if (s1 > s2)
            {
                int t1; int t2;
                t1 = s1; t2 = e1;
                s1 = s2; e1 = e2;
                s2 = t1; e2 = t2;
            }

            /* s1 ---------------e1 */ 
            /* ------ s2-----------------e2 */

            if (s1 <= s2 && s2 <= e1)
            {
                return true; // overlapped found
            }

            return false;
        }

        private int ConvertStringToTime(string startingTime)
        {
            string hr, mn;
            
            hr = "";
            mn = ""; 

            int flag = 0; 

            for (int i = 0; i < startingTime.Length; i++)
            {
                if (flag == 0 && i != startingTime.Length && (startingTime[i] == '.'  ) )
                {
                    flag = 1; 
                }
                if (flag == 0) hr += startingTime[i];
                else mn += startingTime[i]; 
            }

            int totaltime = 0;

            int val = 0; 
            
            for (int i = 0; i < hr.Length; i++)
            {
                if (i > 0)
                {
                    val = val*10 + (hr[i] - '0');
                }
                else
                {
                    val = hr[i] - '0';
                }
            }

            totaltime = val*60;

            val = 0;
            
            for (int i = 0; i < mn.Length; i++)
            {
                if (i > 0)
                {
                    val = val * 10 + (mn[i] - '0');
                }
                else
                {
                    val = mn[i] - '0';
                }
            }

            totaltime += val;

            return totaltime;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult ClassSchedule(Department aDepartment)
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            List<Course> courses =
                (from c in db.Courses where c.Department.DepartmentId == aDepartment.DepartmentId select c).ToList();
            List<AllocateRoom> allocations = db.AllocateRooms.ToList();
            
            List<string> code = new List<string>();
            List<string> name = new List<string>();
            List<string> schdule = new List<string>();

            foreach (Course course in courses)
            {
                string message = "";
                foreach (AllocateRoom allocation in allocations)
                {
                    int flag = 0; 
                    if (course.CourseId == allocation.CourseId)
                    {
                        if (flag == 0)
                        {
                            message += "R-No: ";
                            flag = 1;
                        }
                        
                        Room aRoom = (from r in db.Rooms where r.RoomId == allocation.RoomId select r).SingleOrDefault();
                        Day day = (from d in db.Days where d.DayId == allocation.DayId select d).SingleOrDefault();

                        allocation.Room = aRoom;
                        allocation.Day = day;

                        message += aRoom.RoomNo + ", " + day.DayName + ", " + allocation.StartingTime + " - " +
                                   allocation.EndingTime + ";";
                    }
                }

                if (message == "") message += "Couse Yet not Scheduled";
                
                schdule.Add(message);
                code.Add(course.CourseCode);
                name.Add(course.CourseName);
            }

            ViewBag.CourseCode = code;
            ViewBag.CourseName = name;
            ViewBag.Schedules = schdule;

            return View();
        }

        public JsonResult IsTimeStringValid(string startingTime)
        {
            bool result = ValidationOfTimeString(startingTime);  
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEndingTimeStringValid(string endingTime)
        {
            bool result = ValidationOfTimeString(endingTime);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private bool ValidationOfTimeString(string timeString)
        {
            try
            {
                bool flag = true;
                int dot = 0;
                int dotPos = -1;



                // anything than 0-9 and dot(.)
                for (int i = 0, j = timeString.Length; i < j; i++)
                {
                    if ((timeString[i] < '0' && timeString[i] > '9') && timeString[i] != '.')
                    {
                        flag = false;
                    }

                    if (timeString[i] == '.')
                    {
                        dot++;
                        dotPos = i;
                    }

                }

                if (!flag)
                {
                    return flag;
                }

                //how many dot 
                if (dot == 1)
                {
                    //where is the dot
                    if (dotPos < 1 || dotPos > 2)
                    {
                        // hh or h . mm or m  
                        flag = false;
                    }

                    // dot at the last 
                    if (dotPos == timeString.Length - 1)
                    {
                        flag = false;
                    }

                    // if mm is more than 2 digit 
                    if (dotPos != 1  && dotPos != 2)
                    {
                        flag = false;
                    }

                }
                else
                {
                    flag = false;
                }

                if (!flag)
                {
                    return flag;
                }

                // invalid time 

                if (dotPos == 1)
                {
                    int hh;
                    int mm;

                    hh = (timeString[0] - '0');

                    if (timeString.Length - dotPos == 1)
                    {
                        mm = timeString[2] - '0';
                    }
                    else
                    {
                        mm = (timeString[2] - '0')*10 + (timeString[3] - '0');
                    }

                    if (hh > 23 || mm > 59)
                    {
                        flag = false;
                    }
                }
                else // hr has 2 digit 
                {
                    int hh;
                    int mm;

                    hh = (timeString[0] - '0')*10 + (timeString[1] - '0');

                    if (dotPos == 1)
                    {
                        mm = timeString[3] - '0';
                    }
                    else
                    {
                        mm = (timeString[3] - '0')*10 + (timeString[4] - '0');
                    }

                    if (hh > 23 || mm > 59)
                    {
                        flag = false;
                    }
                }
                return flag;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}