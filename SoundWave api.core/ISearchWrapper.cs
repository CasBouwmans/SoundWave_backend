namespace SoundWave_api.core
{
    public interface ISearchWrapper
    {
        Task<List<object>> GetSearchResults(string token, string searchTerm, string searchType);
    }
}
