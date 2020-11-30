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
        RestService restService = new RestService();

        public JokePage(Joke joke)
        {
            InitializeComponent();

            _joke = joke;
            ratingSelect.SelectedIndex = joke.rating - 1;
        }

        private async void ratingSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            await updateJokeData(new { rating = (ratingSelect.SelectedIndex + 1) });
        }

        public async Task updateJokeData(object newData)
        {
            string url = $"https://studentloginexample.herokuapp.com/api/jokes/?_id={_joke._id}";
            await restService.UpdateJoke(url, newData);
        }
    }
}