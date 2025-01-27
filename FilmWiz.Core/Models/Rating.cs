namespace FilmWiz.Core.Models
{
    /// <summary>
    /// Represents a rating from a specific source
    /// </summary>
    public record Rating
    {
        /// <summary>
        /// The source of the rating
        /// </summary>
        public string? Source { get; init; }

        /// <summary>
        /// The rating value from this source
        /// </summary>
        public string? Value { get; init; }
    }
}
