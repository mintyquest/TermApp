using System;
using System.Collections.Generic;
using System.Linq;
using TermApp.Models;
using TermApp.Views;
using TermApp.Data;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TermApp.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            
        }

        

        async void DemoData()
        {
            //placeholder data for testing
            var demoData = await App.Database.GetAllTermsAsync();
            
            if (demoData.Any() == false)
            {
                var demoTerm = new Term();
                demoTerm.TermName = "Term 1";
                demoTerm.StartDate = new DateTime(2022, 02, 02);
                demoTerm.EndDate = new DateTime(2022, 5, 02);
                await App.Database.SaveTermAsync(demoTerm);

                var demoCourse = new Course();
                demoCourse.CourseName = "Web Development";
                demoCourse.StartDate = new DateTime(2022, 02, 02);
                demoCourse.EndDate = new DateTime(2022, 02, 14);
                demoCourse.Status = "Started";
                demoCourse.InstructorName = "Marques Taylor";
                demoCourse.InstructorPhone = "909-786-2395";
                demoCourse.InstructorEmail = "mtayl34@wgu.edu";
                
                demoCourse.Notes = "Remember to look at course on Pluralsight for reference.";
                demoCourse.TermId = demoTerm.TermId;
                await App.Database.SaveCourseAsync(demoCourse);
                
                var demoAssessment = new Assessment();
                demoAssessment.AssessmentName = "Web Technology";
                demoAssessment.CourseId = demoCourse.CourseId;
                demoAssessment.DueDate = new DateTime(2022, 02, 12);
                demoAssessment.Type = "Objective Assessment";
                demoAssessment.NotifyIncoming = -1;
                
                var demoAssessment2 = new Assessment();
                demoAssessment2.AssessmentName = "Building a Web App";
                demoAssessment2.CourseId = demoCourse.CourseId;
                demoAssessment2.DueDate = new DateTime(2022, 02, 14);
                demoAssessment2.Type = "Project Assessment";
                demoAssessment2.NotifyIncoming = -1;
                
                await App.Database.SaveAssessmentAsync(demoAssessment);
                await App.Database.SaveAssessmentAsync(demoAssessment2);
                TermView.ItemsSource = await App.Database.GetAllTermsAsync();

            }

        }
        

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            DemoData(); //loads placeholder data for testing

            TermView.ItemsSource = await App.Database.GetAllTermsAsync();
           
            
        }

        async void OnEditClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("TermManager");
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                
                var term = (Term)e.CurrentSelection.FirstOrDefault(); 
                App.termPasserId = term.TermId;
                await Shell.Current.GoToAsync($"{nameof(CoursePage)}?{nameof(CoursePage.OwnerId)}={term.TermId}");
                
            }
        }
    }
}