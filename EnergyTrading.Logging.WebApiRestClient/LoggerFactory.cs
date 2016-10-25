using System;

namespace EnergyTrading.Logging.WebApiRestClient
{
    public class LoggerFactory : ILoggerFactory
    {
        private readonly string baseUri;
        private readonly ILogger exceptionLogger;

        public LoggerFactory(string baseUri, ILogger exceptionLogger = null)
        {
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                throw new ArgumentOutOfRangeException(nameof(baseUri));
            }
            this.baseUri = baseUri;
            this.exceptionLogger = exceptionLogger;
        }

        public ILogger GetLogger(string name)
        {
            return new Logger(baseUri, name, exceptionLogger);
        }

        public ILogger GetLogger<T>()
        {
            return GetLogger(typeof(T));
        }

        public ILogger GetLogger(Type type)
        {
            return GetLogger(type.FullName);
        }

        public void Initialize()
        {
        }

        public void Shutdown()
        {
        }
    }
}