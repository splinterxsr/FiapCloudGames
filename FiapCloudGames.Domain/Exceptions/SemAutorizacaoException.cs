namespace FiapCloudGames.Domain.Exceptions
{
    public class SemAutorizacaoException : Exception
    {
        public SemAutorizacaoException()
        {
        }

        public SemAutorizacaoException(string message) : base(message)
        {
        }

        public SemAutorizacaoException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}