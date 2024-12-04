using Newtonsoft.Json;
using SoundWave_api.core;
using System.Net.Http;

namespace SoundWave_api.ApiWrapper
{
    public class SearchWrapper : ISearchWrapper
    {
        private readonly HttpClient _httpClient;

        public SearchWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.spotify.com/v1/");
        }

        public async Task<List<object>> GetSearchResults(string token, string searchTerm, string searchType)
        {
            // Zorg ervoor dat je 'type' correct doorgeeft (artist, album, track)
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetAsync($"search?q={searchTerm}&type={searchType}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RootObject>(jsonResponse);

                // Hier kun je artiesten, albums of tracks returnen afhankelijk van de searchType
                return searchType switch
                {
                    "artist" => result.Artists.Items.Cast<object>().ToList(),
                    "album" => result.Albums.Items.Cast<object>().ToList(),
                    "track" => result.Tracks.Items.Cast<object>().ToList(),
                    _ => new List<object>()
                };
            }

            return new List<object>();
        }
    }
}
