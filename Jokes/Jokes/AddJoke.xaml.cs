 using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jokes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddJoke : ContentPage
    {
        RestService restService = new RestService();
        Joke newJoke;

        public AddJoke()
        {
            InitializeComponent();
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            loadingAddJoke.IsVisible = true;

            Task.Run(async () => {
                await addJoke();

                Device.BeginInvokeOnMainThread(() => {
                    loadingAddJoke.IsVisible = false;
                    jokeInput.Text = "";
                    Navigation.PopAsync(); // Remove add joke page from navigation stack
                    Navigation.PushAsync(new JokePage(newJoke) { BindingContext = newJoke });
                });
            });
        }

        private async Task addJoke()
        {
            if (jokeInput.Text.Trim().Length == 0) return;

            string url = "https://studentloginexample.herokuapp.com/api/jokes/";
            SingleResponse resp = await restService.AddJoke(url, new { jokeText = jokeInput.Text, rating = 1, numberOfRatings = 1 });
            newJoke = resp.dataResponse;
        }
    }
}