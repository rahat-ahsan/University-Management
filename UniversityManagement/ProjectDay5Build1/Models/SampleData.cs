using System;
using System.Collections.Generic;
using System.Data.Entity;


namespace ProjectDay5Build1.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            new List<Semester>
                {
                    new Semester{SemesterName = "Semester-1"},
                    new Semester{SemesterName = "Semester-2"},
                    new Semester{SemesterName = "Semester-3"},
                    new Semester{SemesterName = "Semester-4"},
                    new Semester{SemesterName = "Semester-5"},
                    new Semester{SemesterName = "Semester-6"},
                    new Semester{SemesterName = "Semester-7"},
                    new Semester{SemesterName = "Semester-8"}
                }.ForEach(s=>context.Semesters.Add(s));
            
            context.SaveChanges();

            new List<Designation>
                {
                    new Designation{DesignationName = "Professor"},
                    new Designation{DesignationName = "Associate Professor"},
                    new Designation{DesignationName = "Assistant Professor"},
                    new Designation{DesignationName = "Senior Lecturer"},
                    new Designation{DesignationName = "Lecturer"}
                }.ForEach(d=>context.Designations.Add(d));
            
            context.SaveChanges();

            new List<Grade>
                {
                    new Grade {GradeCode = "A+"},
                    new Grade {GradeCode = "A"},
                    new Grade {GradeCode = "A-"},

                    new Grade {GradeCode = "B+"},
                    new Grade {GradeCode = "B"},
                    new Grade {GradeCode = "B-"},

                    new Grade {GradeCode = "C+"},
                    new Grade {GradeCode = "C"},

                    new Grade {GradeCode = "D"},
                    new Grade {GradeCode = "F"},
                }.ForEach(g=>context.Grades.Add(g));
            
            context.SaveChanges();

            new List<Day>
                {
                   new Day {DayName = "Monday"},
                   new Day {DayName = "Tuesday"},
                   new Day {DayName = "Wednesday"},
                   new Day {DayName = "Thrusday"},
                   new Day {DayName = "Friday"},
                   new Day {DayName = "Satarday"},
                   new Day {DayName = "Sunday"},
                   
                }.ForEach(d=>context.Days.Add(d));
            
            context.SaveChanges();

            new List<Room>
                {
                    new Room{RoomNo = "B1-1001"},
                    new Room{RoomNo = "B1-1002"},
                    new Room{RoomNo = "B1-1003"},
                    new Room{RoomNo = "B1-1004"},
                    new Room{RoomNo = "B1-1005"},
                    
                    new Room{RoomNo = "B2-1001"},
                    new Room{RoomNo = "B2-1002"},
                    new Room{RoomNo = "B2-1003"},
                    new Room{RoomNo = "B2-1004"},
                    new Room{RoomNo = "B2-1005"}
                }.ForEach(r=>context.Rooms.Add(r));
            
            context.SaveChanges();

            var departments = new List<Department>
                {
                    new Department {DepartmentCode = "CSE", DepartmentName = "Computer Science and Engineering"},
                    new Department {DepartmentCode = "ETE", DepartmentName = "Electronics and Telecommunication Engineering"},
                    new Department {DepartmentCode = "EEE", DepartmentName = "Electrical and Electronics Engineering"}
                }; 
            
            departments.ForEach(d=>context.Departments.Add(d));
            context.SaveChanges();

            new List<Teacher>
                {
                    new Teacher
                        {
                            
                            TeacherName = "R. I. Sharif",
                            TeacherAddress = "Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 1,
                            TeacherEmail = "risharif@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "M. Quamruzzaman",
                            TeacherAddress = "Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 2,
                            TeacherEmail = "mquamruzzaman@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Mr. Syed Ahsanul Kabir",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01727654388",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 3,
                            TeacherEmail = "syedkabir@seu.bd",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Shahriar Manzoor",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01711322375 ",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 4,
                            TeacherEmail = "shahriar_manzoor@yahoo.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Md. Ashiqur Rahman ",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01716603993",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 4,
                            TeacherEmail = "ashiq_rahman@yahoo.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Roksana Akter",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01552404534",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 4,
                            TeacherEmail = "jolly_csdu@yahoo.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Monirul Hasan",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 4,
                            TeacherEmail = "kmhasan@gmail.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Md. Ashraful Hoque",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01723227894",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 5,
                            TeacherEmail = "ashraful@seu.ac.bd",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Shagufta Ashraf",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01815777835",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 5,
                            TeacherEmail = "lifa_ashraf@yahoo.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Nafisa Khanam Siddika",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01727697147",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 5,
                            TeacherEmail = "nafisasiddika@gmail.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Md. Rakibul Hasan",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01818390965 ",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 5,
                            TeacherEmail = "md.rakibul_hasan@yahoo.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Mohammad Shohel Rana",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01753790612",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Md. Rasel Kabir",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "01742251377",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 1,
                            DesignationId = 5,
                            TeacherEmail = "raselsh023@yahoo.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Md. Asaduzzaman Khan",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 4,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Rezwan-Al-Islam Khan",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 4,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Md. Samiul Islam Sadek",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Md. Moniruzzaman",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Mahjabin Mubarak",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Tanzilah Noor Shabnam",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Md. Kamrul Hasan Chowdhury",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Abdul Hamid Bin Yousuf",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 2,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Dr. Munima Haque",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 3,
                            DesignationId = 3,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Neva Agarwala",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 3,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                    new Teacher
                        {
                            TeacherName = "Murad Kabir Nipun",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 3,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                     new Teacher
                        {
                            TeacherName = "Sazia Afreen",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 3,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                     new Teacher
                        {
                            TeacherName = "Abdullah-Al-Manzur",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 3,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        },
                     new Teacher
                        {
                            TeacherName = "Md. Nazmul Islam Khan",
                            TeacherAddress = "A.R. Tower, Banani Dhaka",
                            TeacherContactNo = "12345678901",
                            TeacherCreditToBeTaken = 12,
                            DepartmentId = 3,
                            DesignationId = 5,
                            TeacherEmail = "abc@abc.com",
                            TeacherRemainingCredit =  12
                        }
                }.ForEach(t=>context.Teachers.Add(t));
            context.SaveChanges();

            new List<Course>
                {
                    new Course{CourseCode="CSE101", CourseName="C",                 CourseCredit = 3, DepartmentId = 1, SemesterId = 1, CourseDescription = "Programming Language I (C)"},
                    new Course{CourseCode="CSE102", CourseName="JAVA",              CourseCredit = 3, DepartmentId = 1, SemesterId = 3, CourseDescription = "Programming Language II(Java)"},
                    new Course{CourseCode="CSE103", CourseName="C++",               CourseCredit = 3, DepartmentId = 1, SemesterId = 2, CourseDescription = "Programming Language III(Visual C++)"},
                    new Course{CourseCode="CSE104", CourseName="Assembly",          CourseCredit = 3, DepartmentId = 1, SemesterId = 4, CourseDescription = "Programming Language IV (Assembly)"},
                    new Course{CourseCode="CSE105", CourseName="JSP",               CourseCredit = 3, DepartmentId = 1, SemesterId = 5, CourseDescription = "Internet Programming"},
                    new Course{CourseCode="CSE106", CourseName="Js",                CourseCredit = 3, DepartmentId = 1, SemesterId = 6, CourseDescription = "Javascript"},
                    new Course{CourseCode="CSE107", CourseName="PHP",               CourseCredit = 3, DepartmentId = 1, SemesterId = 7, CourseDescription = "Hypertext Preprocessor"},
                    new Course{CourseCode="CSE108", CourseName="C#",                CourseCredit = 3, DepartmentId = 1, SemesterId = 8, CourseDescription = "C Sharp"},

                    new Course{CourseCode="CSE109", CourseName="Graph",             CourseCredit = 3, DepartmentId = 1, SemesterId = 1, CourseDescription = "Graph Theroy"},
                    new Course{CourseCode="CSE110", CourseName="Formal",            CourseCredit = 3, DepartmentId = 1, SemesterId = 2, CourseDescription = "Formal Language Theroy"},
                    new Course{CourseCode="CSE111", CourseName="Complier",          CourseCredit = 3, DepartmentId = 1, SemesterId = 3, CourseDescription = "Complier Construction"},
                    new Course{CourseCode="CSE112", CourseName="Database",          CourseCredit = 3, DepartmentId = 1, SemesterId = 4, CourseDescription = "Database Design"},
                    new Course{CourseCode="CSE113", CourseName="DS",                CourseCredit = 3, DepartmentId = 1, SemesterId = 5, CourseDescription = "Data Structure"},
                    new Course{CourseCode="CSE114", CourseName="System Analysis",   CourseCredit = 3, DepartmentId = 1, SemesterId = 6, CourseDescription = "System Analysis and Design"},
                    new Course{CourseCode="CSE115", CourseName="Software Eng",      CourseCredit = 3, DepartmentId = 1, SemesterId = 7, CourseDescription = "Software Engineering"},
                    new Course{CourseCode="CSE116", CourseName="AI",                CourseCredit = 3, DepartmentId = 1, SemesterId = 8, CourseDescription = "Artificial Intelligence"},

                    new Course{CourseCode="CSE136",CourseName="Geometry",           CourseCredit = 3, DepartmentId = 1, SemesterId = 4, CourseDescription = "Co-ordinate Geometry"},
                    new Course{CourseCode="CSE137",CourseName="Calculus",           CourseCredit = 3, DepartmentId = 1, SemesterId = 5, CourseDescription = " Calculus and Integral Calculus"},
                    new Course{CourseCode="CSE138",CourseName="ODE PDE",            CourseCredit = 3, DepartmentId = 1, SemesterId = 6, CourseDescription = "Ordinary Differential Equation and Partial Differential Equation"},
                    new Course{CourseCode="CSE139",CourseName="Complex Variables",  CourseCredit = 3, DepartmentId = 1, SemesterId = 7, CourseDescription = "Complex Variables and Transforms"},
                    new Course{CourseCode="CSE140",CourseName="Linear Algebra",     CourseCredit = 3, DepartmentId = 1, SemesterId = 8, CourseDescription = "Linear Algebra and Vector Analysis"}
                }.ForEach(c=>context.Courses.Add(c));

            context.SaveChanges();


            new List<Student>
                {
                    
                    new Student
                        {
                            StudentName = "Moushumi Deb",
                            StudentEmail = "abc@abc.com",
                            StudentContactNo =  "01234567891",
                            StudentRegistrationDate = Convert.ToDateTime("2013-07-13 00:00:00.000"),
                            StudentAddress = "Dhaka",
                            Department = departments[0],
                            StudentRegistrationNo = "CSE-2013-001"

                        },

                    new Student
                        {
                            StudentName = "Monowar Hossain",
                            StudentEmail = "abc@abc.com",
                            StudentContactNo =  "01234567891",
                            StudentRegistrationDate = Convert.ToDateTime("2013-07-13 00:00:00.000"),
                            StudentAddress = "Dhaka",
                            Department = departments[0],
                            StudentRegistrationNo = "CSE-2013-002"

                        },
                    new Student
                        {
                            StudentName = "Jibonanodo Sana",
                            StudentEmail = "abc@abc.com",
                            StudentContactNo =  "01234567891",
                            StudentRegistrationDate = Convert.ToDateTime("2013-07-13 00:00:00.000"),
                            StudentAddress = "Dhaka",
                            Department = departments[0],
                            StudentRegistrationNo="CSE-2013-003"

                        },
                    new Student
                        {
                            StudentName = "Mizanur Rahman",
                            StudentEmail = "abc@abc.com",
                            StudentContactNo =  "01234567891",
                            StudentRegistrationDate = Convert.ToDateTime("2013-07-13 00:00:00.000"),
                            StudentAddress = "Dhaka",
                            Department = departments[0],
                            StudentRegistrationNo="CSE-2013-004"

                        },
                    new Student
                        {
                            StudentName = "Jahid Hossain Jibon",
                            StudentEmail = "abc@abc.com",
                            StudentContactNo =  "01234567891",
                            StudentRegistrationDate = Convert.ToDateTime("2013-07-13 00:00:00.000"),
                            StudentAddress = "Dhaka",
                            Department = departments[0],
                            StudentRegistrationNo="CSE-2013-005"

                        },
                    new Student
                        {
                            StudentName = "Anamul Habib Tushar",
                            StudentEmail = "abc@abc.com",
                            StudentContactNo =  "01234567891",
                            StudentRegistrationDate = Convert.ToDateTime("2013-07-13 00:00:00.000"),
                            StudentAddress = "Dhaka",
                            Department = departments[0],
                            StudentRegistrationNo="CSE-2013-006"

                        },
                        new Student
                        {
                            StudentName = "Razia Sultana",
                            StudentEmail = "abc@abc.com",
                            StudentContactNo =  "01234567891",
                            StudentRegistrationDate = Convert.ToDateTime("2013-07-13 00:00:00.000"),
                            StudentAddress = "Dhaka",
                            Department = departments[2],
                            StudentRegistrationNo = "EEE-2013-001"

                        },
                    new Student
                        {
                            StudentName = "Ashaduzzaman Rubel",
                            StudentEmail = "abc@abc.com",
                            StudentContactNo =  "01234567891",
                            StudentRegistrationDate = Convert.ToDateTime("2013-07-13 00:00:00.000"),
                            StudentAddress = "Dhaka",
                            Department = departments[1],
                            StudentRegistrationNo="ETE-2013-001"

                        },
                    new Student
                        {
                            StudentName = "Alamgir Hossain",
                            StudentEmail = "abc@abc.com",
                            StudentContactNo =  "01234567891",
                            StudentRegistrationDate = Convert.ToDateTime("2013-07-13 00:00:00.000"),
                            StudentAddress = "Dhaka",
                            Department = departments[1],
                            StudentRegistrationNo="ETE-2013-002"

                        }
                    
                }.ForEach(a => context.Students.Add(a));
        }
    }
}