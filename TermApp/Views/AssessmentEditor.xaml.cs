using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using TermApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermApp.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class AssessmentEditor : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadAssessment(value);
            }
        }
        public int PasserId { get; set; }
        
        async void LoadAssessment(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                
                Assessment assessment = await App.Database.GetAssessmentAsync(id);
                if (assessment.NotifyIncoming >= 0)
                {
                    CbNotify.IsChecked = true;
                }
                else
                {
                    CbNotify.IsChecked = false;
                }
                BindingContext = assessment;
                PasserId = assessment.AssessmentId;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load assessment.");
            }
        }
        
        
        public AssessmentEditor()
        {
            InitializeComponent();
            
        }
        
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var assessment = new Assessment();
            if (PasserId > 0)
            {
                assessment.AssessmentId = PasserId;
            }
            
            assessment.AssessmentName = AssessmentNameEditor.Text;
            assessment.DueDate = DueDatePicker.Date;
            assessment.Type = (string) AssessmentTypePicker.SelectedItem;
            assessment.CourseId = App.coursePasserId;
            assessment.NotifyIncoming = assessment.CourseId + assessment.AssessmentId;
            
            var notifyId = assessment.NotifyIncoming;
            
            if (!string.IsNullOrWhiteSpace(AssessmentNameEditor.Text) && assessment.Type != null)
            {
                
                
                if (CbNotify.IsChecked == true)
                {
                    await App.Database.SaveAssessmentAsync(assessment); 
                    
                    var notification = new NotificationRequest();
                    notification.NotificationId = notifyId;
                    notification.Title = "Reminder";
                    notification.Description = "The "+AssessmentNameEditor.Text+" "+AssessmentTypePicker.SelectedItem.ToString()+" is due on "+DueDatePicker.Date;
                    notification.Schedule.NotifyTime = DateTime.Now.AddSeconds(10); //Set to show notification in ten seconds for testing purposes
                    notification.Android.IconSmallName.ResourceName = "baseline_notification_important_24";
                    notification.Android.ChannelId = "my_channel_01";
                    notification.Android.Priority = AndroidNotificationPriority.Max;
                    notification.Android.VisibilityType = AndroidVisibilityType.Public;
                    await NotificationCenter.Current.Show(notification);
                
                }
                else
                {
                    
                    NotificationCenter.Current.Cancel(notifyId);
                    assessment.NotifyIncoming = -1;
                    await App.Database.SaveAssessmentAsync(assessment); 
                }
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert ("Alert", "Please enter all information", "OK");
            }
            
        }
        
        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var assessment = (Assessment)BindingContext;
            await App.Database.DeleteAssessmentAsync(assessment);

            // Navigate backwards
            await Shell.Current.GoToAsync("../..");
        }
    }
}