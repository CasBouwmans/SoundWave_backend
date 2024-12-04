namespace SoundWave_api.core
{
    public interface ILoginWrapper
{
    string GetSpotifyAuthUrl();
    Task<string> GetAccessToken(string code);
}


}
