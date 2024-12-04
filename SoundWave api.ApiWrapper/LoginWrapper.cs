using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SoundWave_api.core;

namespace SoundWave_api.ApiWrapper
{
    public class LoginWrapper : ILoginWrapper
{
    private readonly string _clientId = "a09667c15c22466f8ea2f0363cf98617";
    private readonly string _clientSecret = "b4ec5d61425a421c9d6a7f886b5457c0";
    private readonly string _redirectUri = "http://localhost:7088/api/login/callback";

    public string GetSpotifyAuthUrl()
    {
        var scopes = "user-read-private,user-read-email,playlist-read-private,playlist-read-collaborative";
        return $"https://accounts.spotify.com/authorize?response_type=code&client_id={_clientId}&scope={scopes}&redirect_uri={_redirectUri}";
    }

    public async Task<string> GetAccessToken(string code)
    {
        using var client = new HttpClient();
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
        tokenRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}")));
        tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "code", code },
            { "redirect_uri", _redirectUri }
        });

        var response = await client.SendAsync(tokenRequest);
        return await response.Content.ReadAsStringAsync(); // Token-informatie als JSON-string
    }
}

}
