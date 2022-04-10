using System;
using SQLite;

namespace TermApp.Models
{
    public class Term
    {
        
        
        [PrimaryKey, AutoIncrement]
        public int TermId { get; set; }
        public string TermName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        
        /*
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public Term()
        {
            
        }*/
    }
}