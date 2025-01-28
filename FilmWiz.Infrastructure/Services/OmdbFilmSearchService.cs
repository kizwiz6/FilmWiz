using FilmWiz.Core.Interfaces;
using FilmWiz.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FilmWiz.Infrastructure.Services
{
    public class OmdbFilmSearchService : IFilmSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OmdbFilmSearchService> _logger;
        private readonly string _apiKey;

        public OmdbFilmSearchService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<OmdbFilmSearchService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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