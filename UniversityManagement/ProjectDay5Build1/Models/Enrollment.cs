using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectDay5Build1.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }
        
        public int? GradeId { get; set; }
        public Grade Grade { get; set; }

    }
}