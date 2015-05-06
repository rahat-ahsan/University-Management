using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ProjectDay5Build1.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please Enter the Name of Student")]
        public string StudentName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please Enter the Email of Student")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "Invalid Email Adress, try again")]
        public string StudentEmail { get; set; }
        
        [DisplayName("Contact No")]
        [Required(ErrorMessage = "Please Enter the Contact No")]
        public string StudentContactNo { get; set; }
        
        [DisplayName("Date of Reg ")]
        [Required(ErrorMessage = "Please Enter Date of Registration (format: MM/DD/YYYY)")]
        [DataType(DataType.DateTime)]
        public DateTime StudentRegistrationDate { get; set; }
        
        [DisplayName("Address")]
        [Required(ErrorMessage = "Please Add Adrress of Studnet")]
        [DataType(DataType.MultilineText)]
        public string StudentAddress { get; set; }

        public string StudentRegistrationNo { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}