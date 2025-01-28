using FilmWiz.Core.Interfaces;
using FilmWiz.Core.Models;
using FilmWiz.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

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
        public async Task<IEnumerable<FilmItem>> SearchFilmsAsync(
            FilmSearchParameters parameters,
            CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Searching for films with term: {SearchTerm}",
                    parameters.SearchTerm);

                var query = $"?apikey={_apiKey}&s={Uri.EscapeDataString(parameters.SearchTerm)}";
                var response = await _httpClient.GetFromJsonAsync<OmdbSearchResponse>(
                    query, cancellationToken);

                return response?.Search?.Select(MapToFilmItem) ?? Enumerable.Empty<FilmItem>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching for films");
                throw;
            }
        }

        private static FilmItem MapToFilmItem(OmdbSearchItem item) =>
            new()
            {
                Title = item.Title,
                Year = item.Year,
                ImdbId = item.ImdbId
            };
    }
}