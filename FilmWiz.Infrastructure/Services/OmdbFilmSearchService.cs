using FilmWiz.Core.Interfaces;
using FilmWiz.Core.Models;
using Microsoft.Extensions.Configuration;

namespace FilmWiz.Infrastructure.Services
{
    public class OmdbFilmSearchService : IFilmSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OmdbFilmSearchService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OMDb:ApiKey"]
                ?? throw new InvalidOperationException("OMDb API key not found");
            _httpClient.BaseAddress = new Uri("https://www.omdbapi.com/");
        }

        public Task<IEnumerable<FilmItem>> SearchFilmsAsync(
            FilmSearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}