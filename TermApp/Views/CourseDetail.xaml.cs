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
    public partial class CourseDetail : ContentPage
    
    {
        
        public int OwnerId
        {

            set => LoadCourse(value);
        }

        public int PasserId { get; set; }
        
        async void LoadCourse(int ownerId)
        {
            if (App.Database.GetCourseAsync(ownerId) != null)
            {
                try
                {
                    var course = await App.Database.GetCourseAsync(ownerId);
                    BindingContext = course;
                    

                    PasserId = ownerId;
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to load courses.");
                }
            }
        }
        
        public CourseDetail()
        {
            InitializeComponent();
            NotesField.IsVisible = false;
            ShareButton.IsVisible = false;

        }
        
        async void OnEditClicked(object sender, EventArgs e)
        {
            
            await Shell.Current.GoToAsync($"{nameof(CourseEditor)}?{nameof(CourseEditor.ItemId)}={PasserId.ToString()}");
        }
        
        
        async void OnDisplayNotesSwitchClicked(object sender, EventArgs e)
        {

            if (NotesField.IsVisible == false)
            {
                NotesField.IsVisible = true;
                ShareButton.IsVisible = true;
                
            }
            else
            {
                NotesField.IsVisible = false;
                ShareButton.IsVisible = false;
                
            }
        }

        async void OnShareButtonClicked(object sender, EventArgs e)
        {
            var sharing = new Sharer();
            await sharing.ShareNotes(NotesField.Text);
        }
        
        async void OnAssessmentsButtonClicked(object sender, EventArgs e)
        {
        await Shell.Current.GoToAsync($"{nameof(AssessmentList)}?{nameof(AssessmentList.OwnerId)}={PasserId.ToString()}");
        
        }
    }
}