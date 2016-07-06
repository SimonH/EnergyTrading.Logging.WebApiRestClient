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
            this.ServiceUri = new Uri(new Uri(baseUri), "api/logs").ToString();
        }

        protected string ServiceUri { get; }

        public async Task<LogMessage> PostAsync(LogMessage message)
        {
            return await this.PostAsync(ServiceUri, message);
        }
    }
}