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

        public Joke(string _id, string jokeText, int rating)
        {
            this._id = _id;
            this.jokeText = jokeText;
            this.rating = rating;
        }
    }
}
