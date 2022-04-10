using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using SQLite;
using TermApp.Models;

namespace TermApp.Data
{
    public class CourseDb
    {
        public static string ConnectText = "to";
         
        readonly SQLiteAsyncConnection database;

        public CourseDb(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Term>().Wait();
            database.CreateTableAsync<Course>().Wait();
            database.CreateTableAsync<Assessment>().Wait();
            
        }

        public Task<List<Term>> GetAllTermsAsync()
        {
            //Get all terms.
            return database.Table<Term>().ToListAsync();
        }
        

        public Task<Term> GetTermAsync(int id)
        {
            // Get a specific term.
            return database.Table<Term>()
                .Where(i => i.TermId == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveTermAsync(Term term)
        {
            if (term.TermId != 0)
            {
                // Update an existing term.
                return database.UpdateAsync(term);
            }
            else
            {
                // Save a new term.
                return database.InsertAsync(term);
            }
        }
        
        

        public Task<int> DeleteTermAsync(Term term)
        {
            // Delete a note.
            return database.DeleteAsync(term);
        }
        
        
        public Task<List<Course>> GetAllCoursesAsync(int id)
        {
            //Get all courses for selected term.
            return database.Table<Course>()
                .Where(i => i.TermId == id).ToListAsync();
            //.FirstOrDefaultAsync();
        }
        /*
        public Task<int> DeleteTermCourses(int id)
        {
            return database.Table<Course>()
                .DeleteAsync(i => i.TermId == id);
        }
        
        public Task<int> DeleteTermAssessments(int id)
        {
            return database.Table<Assessment>()
                .DeleteAsync(i => i.CourseId == id);
        }
        */
        public Task<Course> GetCourseAsync(int id)
        {
            //Retrieves specific course
            return database.Table<Course>()
                .Where(i => i.CourseId == id)
                .FirstOrDefaultAsync();

        }
        
        public Task<int> CountCoursesAsync(int id)
        {
            //Retrieves specific course
            return database.Table<Course>()
                    .Where(i => i.TermId == id)
                    .CountAsync()
                ;

        }
        
        public Task<int> CountAssessmentsAsync(int id)
        {
            //Retrieves specific course
            return database.Table<Assessment>()
                    .Where(i => i.CourseId == id)
                    .CountAsync()
                ;

        }
        
        public Task<int> CountAssessmentsOaAsync(int id)
        {
            //Retrieves specific course
            return database.Table<Assessment>()
                    .Where(i => i.Type == "Objective Assessment" && i.CourseId == id)
                    .CountAsync()
                ;

        }
        
        public Task<int> CountAssessmentsPaAsync(int id)
        {
            //Retrieves specific course
            return database.Table<Assessment>()
                    .Where(i => i.Type == "Project Assessment" && i.CourseId == id)
                    .CountAsync()
                ;

        }
        
        public Task<int> SaveCourseAsync(Course course)
        {
            if (course.CourseId != 0)
            {
                // Update an existing course.
                return database.UpdateAsync(course);
            }
            else
            {
                // Save a new course.
                return database.InsertAsync(course);
            }
        }
        
        public Task<int> DeleteCourseAsync(Course course)
        {
            // Delete a note.
            return database.DeleteAsync(course);
        }
        
        //Assessments
        
        public Task<List<Assessment>> GetAllAssessmentsAsync(int id)
        {
            //Get all assessments for selected course.
            return database.Table<Assessment>()
                .Where(i => i.CourseId == id).ToListAsync();
            //.FirstOrDefaultAsync();
        }
        
        public Task<Assessment> GetAssessmentAsync(int id)
        {
            //Retrieves specific course
            return database.Table<Assessment>()
                .Where(i => i.AssessmentId == id)
                .FirstOrDefaultAsync();

        }
        
        public Task<int> SaveAssessmentAsync(Assessment assessment)
        {
            if (assessment.AssessmentId != 0)
            {
                // Update an existing course.
                return database.UpdateAsync(assessment);
            }
            else
            {
                // Save a new course.
                return database.InsertAsync(assessment);
            }
        }
        
        public Task<int> DeleteAssessmentAsync(Assessment assessment)
        {
            // Delete a note.
            return database.DeleteAsync(assessment);
        }
        
        
    }
}