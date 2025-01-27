namespace FilmWiz.Core.Models
{
    /// <summary>
    /// Parameters for searching films
    /// </summary>
    public record FilmSearchParameters
    {
        /// <summary>
        /// The search term to look for
        /// </summary>
        public string SearchTerm { get; init; } = string.Empty;

        /// <summary>
        /// Optional year of release
        /// </summary>
        public string? Year { get; init; }

        /// <summary>
        /// Page number for pagination
        /// </summary>
        public int Page { get; init; } = 1;
    }
}
