using FilmWiz.Core.Models;

namespace FilmWiz.Core.Interfaces
{
    /// <summary>
    /// Defines operations for searching films
    /// </summary>
    public interface IFilmSearchService
    {
        /// <summary>
        /// Searches for films based on the provided parameters
        /// </summary>
        /// <param name="parameters">Search parameters</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A collection of film items matching the search criteria</returns>
        Task<IEnumerable<FilmItem>> SearchFilmsAsync(
            FilmSearchParameters parameters,
            CancellationToken cancellationToken = default);
    }
}
