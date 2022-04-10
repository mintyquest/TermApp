using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TermApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermApp.Views
{
    
    public partial class CourseAdder : ContentPage
    {
        
        public CourseAdder()
        {
            InitializeComponent();
        }
        
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var course = new Course();
            var counter = App.Database.CountCoursesAsync(App.termPasserId);

            if (counter.Result > 5)
            {
                await DisplayAlert ("Alert", "You cannot add more than six courses in a particular term.", "OK");
            }
            else
            {
                
                

                course.CourseName = CourseNameEditor.Text;
                course.StartDate = StartDatePicker.Date;
                course.EndDate = EndDatePicker.Date;
                course.Status = (string) CourseStatusPicker.SelectedItem;
                course.Notes = NotesEditor.Text;
                course.InstructorName = InstructorNameEditor.Text;
                course.InstructorPhone = InstructorPhoneEditor.Text;
                course.InstructorEmail = InstructorEmailEditor.Text;
                course.TermId = App.termPasserId;

                if (!string.IsNullOrWhiteSpace(CourseNameEditor.Text) && course.Status != null && !string.IsNullOrWhiteSpace(InstructorNameEditor.Text) && !string.IsNullOrWhiteSpace(InstructorPhoneEditor.Text) && !string.IsNullOrWhiteSpace(InstructorEmailEditor.Text))
                {
                    await App.Database.SaveCourseAsync(course); //was SaveCourseAsync
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await DisplayAlert ("Alert", "Please enter all required information, with the exception of Notes which are optional.", "OK");
                }


                
            }
        }

        
    }
}