using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jokes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JokePage : ContentPage
    {
        private Joke _joke;
        int numberOfJokeRatings = 0;
        int totalJokeRating = 0;

        RestService restService = new RestService();

        public JokePage(Joke joke)
        {
            InitializeComponent();

            _joke = joke;
            totalJokeRating = joke.rating;
            numberOfJokeRatings = joke.numberOfRatings == 0 ? 1 : joke.numberOfRatings;
            ratingSelect.SelectedIndex = (joke.rating - 1) / numberOfJokeRatings;
        }

        private async void ratingSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        public async Task updateJokeData(object newData)
        {
            string url = $"https://studentloginexample.herokuapp.com/api/jokes/?_id={_joke._id}";
            await restService.UpdateJoke(url, newData);
        }

        async void submitRating_Clicked(System.Object sender, System.EventArgs e)
        {
            await updateJokeData(new
            {
                rating = (totalJokeRating + (ratingSelect.SelectedIndex + 1)),
                numberOfRatings = numberOfJokeRatings + 1
            });
            Device.BeginInvokeOnMainThread(() =>
            {
                Navigation.PopAsync();
            });

        }
    }
}