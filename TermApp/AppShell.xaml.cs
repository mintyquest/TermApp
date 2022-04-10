using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermApp.Models;
using TermApp.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TermApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute("TermManager", typeof(TermPage));
            Routing.RegisterRoute(nameof(CourseAdder), typeof(CourseAdder));
            Routing.RegisterRoute(nameof(CourseEditor), typeof(CourseEditor));
            Routing.RegisterRoute(nameof(CoursePage), typeof(CoursePage));
            Routing.RegisterRoute(nameof(CourseDetail), typeof(CourseDetail));
            Routing.RegisterRoute(nameof(AssessmentList), typeof(AssessmentList));
            Routing.RegisterRoute(nameof(AssessmentDetail), typeof(AssessmentDetail));
            Routing.RegisterRoute(nameof(AssessmentEditor), typeof(AssessmentEditor));
            Routing.RegisterRoute(nameof(AssessmentAdder), typeof(AssessmentAdder));
            Routing.RegisterRoute(nameof(TermEntryPage), typeof(TermEntryPage));
        }
    }
}