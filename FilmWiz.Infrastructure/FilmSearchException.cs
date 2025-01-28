namespace FilmWiz.Infrastructure
{
    /// <summary>
    /// Exception thrown when film search operations fail
    /// </summary>
    public class FilmSearchException : Exception
    {
        public FilmSearchException(string message, Exception? innerException = null)
            : base(message, innerException)
        {
        }
    }
}
