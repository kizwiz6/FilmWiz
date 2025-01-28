using FilmWiz.Core.Interfaces;
using FilmWiz.Core.Models;
using FilmWiz.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace FilmWiz.Infrastructure.Services
{
    /// <summary>
    /// Service for searching films using the OMDb API
    /// </summary>
    public class OmdbFilmSearchService : IFilmSearchService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly ILogger<OmdbFilmSearchService> _logger;
        private readonly string _apiKey;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of the OmdbFilmSearchService
        /// </summary>
        /// <param name="httpClient">The HTTP client for making API requests</param>
        /// <param name="configuration">The configuration containing the API key</param>
        /// <param name="logger">The logger instance</param>
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
        #endregion

        #region Public Methods
        /// <inheritdoc/>
        public async Task<IEnumerable<FilmItem>> SearchFilmsAsync(
    FilmSearchParameters parameters,
    CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Searching for films with term: {SearchTerm}",
                    parameters.SearchTerm);

                var query = BuildSearchQuery(parameters);
                var response = await _httpClient.GetAsync(query, cancellationToken);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    _logger.LogError("API key is invalid or missing");
                    throw new FilmSearchException("Invalid API key. Please check your configuration.");
                }

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<OmdbSearchResponse>(
                    cancellationToken: cancellationToken);

                if (result?.Search == null)
                {
                    _logger.LogWarning("No results found for search term: {SearchTerm}",
                        parameters.SearchTerm);
                    return Enumerable.Empty<FilmItem>();
                }

                return result.Search.Select(MapToFilmItem);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while calling OMDb API");
                throw new FilmSearchException($"Failed to connect to film database: {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error parsing OMDb API response");
                throw new FilmSearchException("Failed to process film data", ex);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Builds the query string for the OMDb API request
        /// </summary>
        /// <param name="parameters">The search parameters to include in the query</param>
        /// <returns>A formatted query string</returns>
        private string BuildSearchQuery(FilmSearchParameters parameters)
        {
            var query = $"?apikey={_apiKey}&s={Uri.EscapeDataString(parameters.SearchTerm)}";

            if (!string.IsNullOrEmpty(parameters.Year))
                query += $"&y={parameters.Year}";

            if (parameters.Page > 1)
                query += $"&page={parameters.Page}";

            _logger.LogInformation("Making request to OMDb API with search term: {SearchTerm}", parameters.SearchTerm);
            return query;
        }

        /// <summary>
        /// Maps an OMDb search item to a FilmItem domain model
        /// </summary>
        /// <param name="item">The OMDb search item to map</param>
        /// <returns>A mapped FilmItem</returns>
        private static FilmItem MapToFilmItem(OmdbSearchItem item) =>
            new()
            {
                Title = item.Title,
                Year = item.Year,
                ImdbId = item.ImdbId
            };
        #endregion
    }
}