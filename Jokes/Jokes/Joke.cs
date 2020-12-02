using System;
using Newtonsoft.Json;

namespace Jokes
{
    [Serializable]
    public class Joke
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("jokeText")]
        public string jokeText { get; set; }
        [JsonProperty("rating")]
        public int rating { get; set; }
        [JsonProperty("numberOfRatings")]
        public int numberOfRatings { get; set; }

        public Joke(string _id, string jokeText, int rating, int numberOfRatings)
        {
            this._id = _id;
            this.jokeText = jokeText;
            this.rating = rating;
            this.numberOfRatings = numberOfRatings;
        }

        public string Description
        {
            get {
                numberOfRatings = numberOfRatings == 0 ? 1 : numberOfRatings;
                int avgRating = rating / numberOfRatings;
                return $"{jokeText} - Rating: {avgRating}";
            }
        }
    }
}
