using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ProjectDay5Build1.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please Enter the Name of Teacher")]
        public string TeacherName { get; set; }
        
        [DisplayName("Address")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please Enter the Address of Teacher")]
        public string TeacherAddress { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please Enter the Email of Teacher")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",ErrorMessage = "Invalid Email Adress, try again")]
        public string TeacherEmail { get; set; }
        
        [DisplayName("Contact No:")]
        [Required(ErrorMessage = "Please Enter the Contact No. of Teacher")]
        public string TeacherContactNo { get; set; }
        
        [DisplayName("Credit to be taken")]
        [Required(ErrorMessage = "Please fill up this box")]
        public double TeacherCreditToBeTaken { get; set; }

        [DisplayName("Remaining Credit")]
        public double TeacherRemainingCredit { get; set; }

        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}