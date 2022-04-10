using System.Threading.Tasks;
using Xamarin.Essentials;

namespace TermApp
{
    public class Sharer
    {
        public async Task ShareNotes(string txt)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = txt,
                Title = "Share Notes"
            });
        }

        
    }
}