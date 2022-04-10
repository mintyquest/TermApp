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
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class TermEntryPage : ContentPage
    {
        
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }
        public TermEntryPage()
        {
            InitializeComponent();
            BindingContext = new Term();
            //BindingContext = LoadNote(ItemId);
        }
        
        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Term term = await App.Database.GetTermAsync(id);
                BindingContext = term;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load term.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var term = (Term)BindingContext;
            if (!string.IsNullOrWhiteSpace(term.TermName))
            {
                await App.Database.SaveTermAsync(term);
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert ("Alert", "Please enter a name for the term", "OK");
            }

            
            
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var term = (Term)BindingContext;

            await App.Database.DeleteTermAsync(term);
            

            //Go back a page
            await Shell.Current.GoToAsync("..");
        }
    }
}