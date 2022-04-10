using System;
using SQLite;

namespace TermApp.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentId { get; set; }
        public int CourseId { get; set; }
        public string AssessmentName { get; set; }
        
        public DateTime DueDate { get; set; } 
        public string Type { get; set; }
        public int NotifyIncoming { get; set; }
    }
}