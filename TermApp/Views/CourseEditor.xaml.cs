using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class CourseEditor : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadCourse(value);
            }
        }
        public int PasserId { get; set; }
        
        async void LoadCourse(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                
                Course course = await App.Database.GetCourseAsync(id);
                
                BindingContext = course;
                PasserId = course.CourseId;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load course.");
            }
        }
        public CourseEditor()
        {
            InitializeComponent();
        }
        
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var course = new Course();
            
            course.CourseId = PasserId;
            
            
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
                    await App.Database.SaveCourseAsync(course);
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await DisplayAlert ("Alert", "Please enter all required information, with the exception of Notes which are optional.", "OK");
                }

        }
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var course = (Course)BindingContext;
            await App.Database.DeleteCourseAsync(course);

            ////Go back two pages
            await Shell.Current.GoToAsync("../..");
        }

        
    }
}