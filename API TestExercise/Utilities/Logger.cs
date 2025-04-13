using Microsoft.Extensions.Logging;

namespace API_TestExercise.Utilities
{
    internal class Logger<T>
    {
        private readonly ILogger<T> _logger;

        public Logger(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void LogError(string message, Exception ex)
        {
            _logger.LogError(ex, message);
        }

        public void LogDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void LogCritical(string message, Exception ex)
        {
            _logger.LogCritical(ex, message);
        }
    }
}
