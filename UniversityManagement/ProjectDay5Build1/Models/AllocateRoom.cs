using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectDay5Build1.Models
{
    public class AllocateRoom
    {
        public int AllocateRoomId { get; set; }
        
        public Department Department { get; set; }
        
        public int CourseId{ get; set; }
        public Course Course { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public int DayId { get; set; }
        public Day Day { get; set; }

        [DisplayName("Starting Time 'hh.mm' (24hr format)")]
        [Required(ErrorMessage = "Please Enter Starting Time in 'hh.mm' format")]
        [Remote("IsTimeStringValid","AllocateRoom", ErrorMessage = ("Please insert a valid time between (00.00 to 23.59)"))]
        public string StartingTime { get; set; }

        [DisplayName("Ending Time 'hh.mm' (24hr format)")]
        [Required(ErrorMessage = "Please Enter Ending in 'hh.mm' format")]
        [Remote("IsEndingTimeStringValid", "AllocateRoom", ErrorMessage = ("Please insert a valid time between (00.00 to 23.59) and greater than starting time"))]
        public string EndingTime { get; set; }
    }
}