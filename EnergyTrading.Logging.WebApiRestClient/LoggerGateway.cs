using System;
using System.Threading.Tasks;
using EnergyTrading.Contracts.Logging;
using EnergyTrading.WebApi.Common.Client;

namespace EnergyTrading.Logging.WebApiRestClient
{
    public class LoggerGateway : ServiceGatewayBase
    {
        public LoggerGateway(string baseUri) : base(baseUri, "api/logs")
        {
        }

        public async Task<LogMessage> PostAsync(LogMessage message)
        {
            return await this.PostAsync<LogMessage, LogMessage>(ServiceUri, message).ConfigureAwait(false);
        }
    }
}