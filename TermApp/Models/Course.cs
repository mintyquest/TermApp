using System;
using SQLite;

namespace TermApp.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }
        public int TermId { get; set; }
        public string CourseName { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public string Status { get; set; }
        public string Notes { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        
    }
}