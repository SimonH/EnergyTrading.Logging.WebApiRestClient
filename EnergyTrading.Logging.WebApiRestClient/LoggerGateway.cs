using System;
using System.Threading.Tasks;
using EnergyTrading.Contracts.Logging;
using EnergyTrading.WebApi.Common.Client;

namespace EnergyTrading.Logging.WebApiRestClient
{
    public class LoggerGateway : RestGatewayBase
    {
        public LoggerGateway(string baseUri)
        {
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                throw new ArgumentOutOfRangeException(nameof(baseUri));
            }
            new Uri(baseUri); // tests that baseUri is a valid uri string
            this.ServiceUri = baseUri + (baseUri.EndsWith("/") ? string.Empty : "/") + "api/logs";
        }

        protected string ServiceUri { get; }

        public async Task<LogMessage> PostAsync(LogMessage message)
        {
            return await this.PostAsync<LogMessage, LogMessage>(ServiceUri, message).ConfigureAwait(false);
        }
    }
}