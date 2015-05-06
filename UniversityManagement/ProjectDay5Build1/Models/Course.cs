using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDay5Build1.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        
        [DisplayName("Code")]
        [Required(ErrorMessage = "Please Enter the Course Code")]
        [Remote("IsCodeValid","Course",ErrorMessage = "Code already exists")]
        public string CourseCode { get; set; }
        
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please Enter the Course Name")]
        [Remote("IsNameValid","Course",ErrorMessage = "Name already exists")]
        public string CourseName { get; set; }

        [DisplayName("Description")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please Add some description about this course")]
        public string CourseDescription { get; set; }

        [DisplayName("Credit")]
        [Required(ErrorMessage = "Please enter the credit for this course")]
        public double CourseCredit { get; set; }
        
        public int DepartmentId { get; set; }
        [DisplayName("Department")]
        public Department Department { get; set; }
        
        
        public int SemesterId { get; set; }
        [DisplayName("Semeter")]
        public Semester Semester { get; set; }

        public Teacher Teacher { get; set; }

       
    }
}