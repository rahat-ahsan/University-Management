using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDay5Build1.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        
        [DisplayName("Code")]
        [Required(ErrorMessage = "Please Enter the Department Code")]
        [Remote("IsCodeValid","Department",ErrorMessage = ("Code is not unique"))]
        public string DepartmentCode { get; set; }
        
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please Enter the Department Name")]
        [Remote("IsNameValid","Department",ErrorMessage = ("Name is not unique"))]
        public string DepartmentName { get; set; }
    }
}