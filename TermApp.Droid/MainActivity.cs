using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.LocalNotification;

namespace TermApp.Droid
{
    [Activity(Label = "TermApp", Theme = "@style/MainTheme", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            NotificationCenter.CreateNotificationChannel(
                new Plugin.LocalNotification.Platform.Droid.NotificationChannelRequest
                {
                    Id = $"my_channel_01",
                    Name = "High",
                    Description = "Will generate a high priority notification",
                    Importance = NotificationImportance.Max,
                });
            
            
            
            /*
            void CreateNotificationChannel()
            {
                if (Build.VERSION.SdkInt < BuildVersionCodes.O)
                {
                    // Notification channels are new in API 26 (and not a part of the
                    // support library). There is no need to create a notification
                    // channel on older versions of Android.
                    return;
                }

                var channelName = "test";
                var channelDescription = "the second try at a notification channel";
                var channel = new NotificationChannel("Higher", channelName, NotificationImportance.High)
                {
                    Description = channelDescription
                };

                var notificationManager = (NotificationManager) GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }*/
            
            LoadApplication(new App());
        }
    }
}