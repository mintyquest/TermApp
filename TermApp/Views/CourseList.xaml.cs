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
    [QueryProperty(nameof(OwnerId), nameof(OwnerId))]
    public partial class CoursePage : ContentPage
    {
        public int OwnerId
        {

            set => LoadCourse(value);
        }

        async void LoadCourse(int ownerId)
        {
            if (App.Database.GetCourseAsync(ownerId) != null)
            {
                try
                {
                    CourseView.ItemsSource = await App.Database.GetAllCoursesAsync(ownerId);
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to load courses.");
                }
            }
        }



        public CoursePage()
        {
            InitializeComponent();
        }
        
        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the Term Manager page.
            await Shell.Current.GoToAsync(nameof(CourseAdder));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                
                var course = (Course)e.CurrentSelection.FirstOrDefault();
                App.coursePasserId = course.CourseId;
                await Shell.Current.GoToAsync($"{nameof(CourseDetail)}?{nameof(CourseDetail.OwnerId)}={course.CourseId}");
                
            }
        }
        
    }
}