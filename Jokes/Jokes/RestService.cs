using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jokes
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<Response> GetUserData(string query)
        {
            Response jokeData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    jokeData = JsonConvert.DeserializeObject<Response>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return jokeData;
        }

        public async Task<Response> UpdateJoke(string url, object newData)
        {
            Response jokeData = null;
            try
            {
                var payload = new StringContent(JsonConvert.SerializeObject(newData), System.Text.Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(url, payload);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    jokeData = JsonConvert.DeserializeObject<Response>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return jokeData;
        }

        public async Task<SingleResponse> AddJoke(string url, object data)
        {
            SingleResponse jokeData = null;
            try
            {
                var payload = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(url, payload);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    jokeData = JsonConvert.DeserializeObject<SingleResponse>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            return jokeData;
        }
    }

    [Serializable]
    public class Response
    {
        [JsonProperty("status")]
        public Boolean status { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("date")]
        public long date { get; set; }

        [JsonProperty("dataResponse")]
        public Joke[] dataResponse { get; set; }
    }

    [Serializable]
    public class SingleResponse
    {
        [JsonProperty("status")]
        public Boolean status { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }

        [JsonProperty("date")]
        public long date { get; set; }

        [JsonProperty("dataResponse")]
        public Joke dataResponse { get; set; }
    }
}