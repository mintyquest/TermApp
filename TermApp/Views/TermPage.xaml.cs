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
    public partial class TermPage : ContentPage
    {
        public TermPage()
        {
            InitializeComponent();
        }
        

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            TermView.ItemsSource = await App.Database.GetAllTermsAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TermEntryPage));
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                Term term = (Term)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(TermEntryPage)}?{nameof(TermEntryPage.ItemId)}={term.TermId.ToString()}");
            }
        }
    }
}