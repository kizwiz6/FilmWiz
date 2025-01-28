using System.Text.Json.Serialization;

namespace FilmWiz.Infrastructure.Models
{
    /// <summary>
    /// Represents a single film item in the OMDb API search results
    /// </summary>
    internal class OmdbSearchItem
    {
        /// <summary>
        /// The title of the film
        /// </summary>
        [JsonPropertyName("Title")]
        public string? Title { get; set; }

        /// <summary>
        /// The year of release
        /// </summary>
        [JsonPropertyName("Year")]
        public string? Year { get; set; }

        /// <summary>
        /// The IMDb identifier for the film
        /// </summary>
        [JsonPropertyName("imdbID")]
        public string? ImdbId { get; set; }
    }
}
