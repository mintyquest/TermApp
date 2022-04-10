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
    public partial class AssessmentDetail : ContentPage
    {
        
        public int OwnerId
        {

            set => LoadAssessment(value);
        }

        public int PasserId { get; set; }
        
        async void LoadAssessment(int ownerId)
        {
            if (App.Database.GetAssessmentAsync(ownerId) != null)
            {
                try
                {
                    var assessment = await App.Database.GetAssessmentAsync(ownerId);
                    BindingContext = assessment;
                    

                    PasserId = ownerId;
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to load assessments.");
                }
            }
        }
        
        public AssessmentDetail()
        {
            InitializeComponent();
        }
        
        async void OnEditClicked(object sender, EventArgs e)
        {
            
            await Shell.Current.GoToAsync($"{nameof(AssessmentEditor)}?{nameof(AssessmentEditor.ItemId)}={PasserId.ToString()}");
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