namespace FilmWiz.Core.Models
{
    /// <summary>
    /// Represents detailed information about a film
    /// </summary>
    public class FilmItem
    {
        /// <summary>
        /// The title of the film or programme
        /// </summary>
        public string? Title { get; set; } 

        /// <summary>
        /// The year of release
        /// </summary>
        public string? Year { get; set; }

        /// <summary>
        /// The release date
        /// </summary>
        public string? Released { get; set; }

        /// <summary>
        /// The runtime in minutes
        /// </summary>
        public string? Runtime { get; set; }

        /// <summary>
        /// Film genres (e.g., "Action, Adventure, Comedy")
        /// </summary>
        public string? Genre { get; set; }

        /// <summary>
        /// The film's director(s)
        /// </summary>
        public string? Director { get; set; }

        /// <summary>
        /// Main cast members
        /// </summary>
        public string? Actors { get; set; }

        /// <summary>
        /// Brief synopsis of the plot
        /// </summary>
        public string? Plot { get; set; }

        /// <summary>
        /// Country of origin
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Metacritic score out of 100
        /// </summary>
        public string? Metascore { get; set; }

        /// <summary>
        /// IMDb rating out of 10
        /// </summary>
        public string? ImdbRating { get; set; }

        /// <summary>
        /// IMDb unique identifier
        /// </summary>
        public string? ImdbId { get; set; }

        /// <summary>
        /// Box office earnings
        /// </summary>
        public string? BoxOffice { get; set; }

        /// <summary>
        /// Production company
        /// </summary>
        public string? Production { get; set; }
    }
}
