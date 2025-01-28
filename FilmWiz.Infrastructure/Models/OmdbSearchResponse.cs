using System.Text.Json.Serialization;

namespace FilmWiz.Infrastructure.Models
{
    /// <summary>
    /// Represents a search response from the OMDb API
    /// </summary>
    internal class OmdbSearchResponse
    {
        /// <summary>
        /// List of films matching the search criteria
        /// </summary>
        [JsonPropertyName("Search")]
        public List<OmdbSearchItem>? Search { get; set; }

        /// <summary>
        /// Response status from the API ("True" or "False")
        /// </summary>
        [JsonPropertyName("Response")]
        public string? Response { get; set; }
    }
}
