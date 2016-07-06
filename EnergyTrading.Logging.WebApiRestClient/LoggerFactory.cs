using System;

namespace EnergyTrading.Logging.WebApiRestClient
{
    public class LoggerFactory : ILoggerFactory
    {
        private readonly string baseUri;

        public LoggerFactory(string baseUri)
        {
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                throw new ArgumentOutOfRangeException(nameof(baseUri));
            }
            this.baseUri = baseUri;
        }

        public ILogger GetLogger(string name)
        {
            return new Logger(baseUri, name);
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