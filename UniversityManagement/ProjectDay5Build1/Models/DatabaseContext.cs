using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectDay5Build1.Models
{
    public class DatabaseContext : DbContext
    {
        
        public DatabaseContext()
            : base("UniversityApp")
        {
            
        }

        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Designation> Designations { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<AllocateRoom> AllocateRooms { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

    }
}