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
    public partial class AssessmentList : ContentPage
    {
        public int OwnerId
        {

            set => LoadAssessments(value);
        }

        async void LoadAssessments(int ownerId)
        {
            if (App.Database.GetAssessmentAsync(ownerId) != null)
            {
                try
                {
                    AssessmentView.ItemsSource = await App.Database.GetAllAssessmentsAsync(ownerId);
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to load assessments.");
                }
            }
        }
        
        
        public AssessmentList()
        {
            InitializeComponent();
        }
        
        async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AssessmentAdder));
        }
        
        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var assessment = (Assessment)e.CurrentSelection.FirstOrDefault(); 
                await Shell.Current.GoToAsync($"{nameof(AssessmentDetail)}?{nameof(AssessmentDetail.OwnerId)}={assessment.AssessmentId}");
                
            }
        }
    }
}