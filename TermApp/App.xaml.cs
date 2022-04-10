using System;
using System.IO;
using Plugin.LocalNotification;
using TermApp.Data;
using TermApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TermApp
{
    public partial class App : Application
    {
        static CourseDb database;

        // Create the database connection as a singleton.
        public static CourseDb Database
        {
            get
            {
                if (database == null)
                {
                    database = new CourseDb(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "appdb.db3")); 
                }
                return database;
            }
        }
        
        public static int termPasserId = 0;
        
        public static int coursePasserId = 0;


        
        
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}