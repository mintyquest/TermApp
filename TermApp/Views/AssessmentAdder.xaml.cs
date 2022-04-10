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
    
    public partial class AssessmentAdder : ContentPage
    {
        
        
        
        public AssessmentAdder()
        {
            InitializeComponent();
        }
        
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var assessment = new Assessment();
            
            var counterAssess = App.Database.CountAssessmentsAsync(App.coursePasserId);
            var counterAssessOa = App.Database.CountAssessmentsOaAsync(App.coursePasserId);
            var counterAssessPa = App.Database.CountAssessmentsPaAsync(App.coursePasserId);

            if (counterAssess.Result > 1)
            {
                await DisplayAlert ("Alert", "You cannot add more than two assessments in a particular course.", "OK");
            }
            else if (counterAssessOa.Result > 0 && AssessmentTypePicker.SelectedItem.ToString() == "Objective Assessment")

            {
                await DisplayAlert ("Alert", "You cannot add more than one Objective Assessment in a particular course.", "OK");
            }
            else if (counterAssessPa.Result > 0 && AssessmentTypePicker.SelectedItem.ToString() == "Project Assessment")
            {
                await DisplayAlert ("Alert", "You cannot add more than one Project Assessment in a particular course.", "OK");
            }
            else
            {


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

                    await App.Database.SaveAssessmentAsync(assessment);
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await DisplayAlert ("Alert", "Please enter all information", "OK");
                }

            }
        }
    }
}