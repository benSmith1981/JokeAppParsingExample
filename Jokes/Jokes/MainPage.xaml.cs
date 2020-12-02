using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Jokes
{
    public partial class MainPage : ContentPage
    {
        RestService restService = new RestService();
        public List<Joke> Jokes = new List<Joke>();
        List<Joke> latestJokes = new List<Joke>();
        List<Joke> ratingJokes = new List<Joke>();

        public MainPage()
        {
            InitializeComponent();

            sortBy.SelectedIndex = 0;
            loadData();
        }

        // Update the data when the page is returned to (i.e. pressing back on a sub page)
        protected override void OnAppearing()
        {
            base.OnAppearing();

            loadData();
        }

        public void loadData()
        {
            listJokes.IsVisible = false;
            loadingList.IsVisible = true;

            Task.Run(async () => {
                await GetJokeData();

                Device.BeginInvokeOnMainThread(() => {
                    loadingList.IsVisible = false;
                    sortBy_SelectedIndexChanged(sortBy, new EventArgs()); // Update list - according to the sort method selected
                    listJokes.IsVisible = true;
                });
            });
        }

        public async Task GetJokeData()
        {
            string url = "https://studentloginexample.herokuapp.com/api/jokes/";
            Response jokeData = await restService.GetUserData(url);
            Jokes = new List<Joke>(jokeData.dataResponse);
            latestJokes = new List<Joke>(jokeData.dataResponse);
            latestJokes.Reverse();
            ratingJokes = new List<Joke>(jokeData.dataResponse).OrderByDescending(j => j.AverageRating).ToList();
        }

        async private void addButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddJoke());
        }

        async private void jokeItem_Tapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new JokePage(e.Item as Joke) { BindingContext = e.Item as Joke });
        }

        private void sortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(sortBy.SelectedItem.ToString())
            {
                case "Oldest":
                    listJokes.ItemsSource = Jokes;
                    break;
                case "Latest":
                    listJokes.ItemsSource = latestJokes;
                    break;
                case "Rating":
                    listJokes.ItemsSource = ratingJokes;
                    break;
            }
        }
    }
}
