using FilmWiz.Core.Models;
using FilmWiz.Infrastructure;
using FilmWiz.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace FilmWiz.Tests;

/// <summary>
/// Tests for the OmdbFilmSearchService
/// </summary>
public class OmdbFilmSearchServiceTests
{
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<ILogger<OmdbFilmSearchService>> _loggerMock;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

    /// <summary>
    /// Initialises a new instance of the OmdbFilmSearchServiceTests class
    /// Sets up mock objects for configuration, logging, and HTTP handling
    /// </summary>
    public OmdbFilmSearchServiceTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _loggerMock = new Mock<ILogger<OmdbFilmSearchService>>();
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        // Setup configuration with test API key
        _configurationMock.Setup(x => x["OMDb:ApiKey"]).Returns("test-api-key");
    }

    /// <summary>
    /// Tests that SearchFilmsAsync returns valid results when given a valid search term
    /// </summary>
    /// <returns>A task representing the asynchronous test operation</returns>
    [Fact]
    public async Task SearchFilmsAsync_WithValidSearch_ReturnsResults()
    {
        // Arrange
        var searchResponse = new
        {
            Search = new[]
            {
                new { Title = "Test Film", Year = "2024", imdbID = "tt1234567" }
            },
            Response = "True"
        };

        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(searchResponse))
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        var service = new OmdbFilmSearchService(httpClient, _configurationMock.Object, _loggerMock.Object);

        // Act
        var result = await service.SearchFilmsAsync(new FilmSearchParameters
        {
            SearchTerm = "Test Film"
        });

        // Assert
        Assert.NotNull(result);
        var films = result.ToList();
        Assert.Single(films);
        Assert.Equal("Test Film", films[0].Title);
        Assert.Equal("2024", films[0].Year);
    }

    /// <summary>
    /// Tests that SearchFilmsAsync returns an empty list when no films are found
    /// </summary>
    /// <returns>A task representing the asynchronous test operation</returns>
    [Fact]
    public async Task SearchFilmsAsync_WithEmptyResponse_ReturnsEmptyList()
    {
        // Arrange
        var searchResponse = new { Response = "False" };
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonSerializer.Serialize(searchResponse))
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        var service = new OmdbFilmSearchService(httpClient, _configurationMock.Object, _loggerMock.Object);

        // Act
        var result = await service.SearchFilmsAsync(new FilmSearchParameters
        {
            SearchTerm = "NonExistentFilm"
        });

        // Assert
        Assert.Empty(result);
    }

    /// <summary>
    /// Tests that SearchFilmsAsync throws a FilmSearchException when the API key is invalid
    /// </summary>
    /// <returns>A task representing the asynchronous test operation</returns>
    [Fact]
    public async Task SearchFilmsAsync_WithInvalidApiKey_ThrowsException()
    {
        // Arrange
        var httpResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.Unauthorized
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        var service = new OmdbFilmSearchService(httpClient, _configurationMock.Object, _loggerMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<FilmSearchException>(() =>
            service.SearchFilmsAsync(new FilmSearchParameters
            {
                SearchTerm = "Test Film"
            }));
    }
}