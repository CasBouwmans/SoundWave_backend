using Microsoft.AspNetCore.Mvc;
using SoundWave_api.core;
using System.Net.Http.Headers;

namespace SoundWave_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginWrapper _loginWrapper;

        public LoginController(ILoginWrapper loginWrapper)
        {
            _loginWrapper = loginWrapper;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var authUrl = _loginWrapper.GetSpotifyAuthUrl();
            return Redirect(authUrl); // Redirect naar Spotify's /authorize endpoint
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code)
        {
            // Gebruik de wrapper om het access token op te halen
            var tokenResponse = await _loginWrapper.GetAccessToken(code);
            return Ok(tokenResponse); // Stuur de tokeninformatie terug naar de front-end
        }
    }


}
