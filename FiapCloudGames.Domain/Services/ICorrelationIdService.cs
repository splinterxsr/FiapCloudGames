namespace FiapCloudGames.Domain.Services
{
    public interface ICorrelationIdService
    {
        string Get();
        void Set(string correlationId);
    }
}
