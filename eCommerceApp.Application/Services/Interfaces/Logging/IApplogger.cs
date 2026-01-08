namespace eCommerceApp.Application.Services.Interfaces.Logging
{
    public interface IApplogger<T>
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(Exception ex, string message);
    }
}
