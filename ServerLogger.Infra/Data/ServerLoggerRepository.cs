using ConsoleApp.Core.Interfaces.Gateways.Repositories.Logs;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogger.Infra.Data
{
    public class ServerLoggerRepository : IServerLoggerRepository
    {
        private readonly ILogger _logger;
        private readonly TelemetryClient _telemetryClient;

        public ServerLoggerRepository(
            ILogger<ServerLoggerRepository> logger,
            TelemetryClient telemetryClient
            )  //TODO: ILogger<T> のTは何らかの型を指定する必要がある
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public Task Info(string message, Type type = null)
        {
            _logger.LogInformation($"Info Message: {message}; Type: {type}");

            //NOTE: WebJobの場合は_telemetryClient.Flush() がないとApplicationInsightsに出力されない
            if (_telemetryClient != null)
            {
                _telemetryClient.Flush();  //AzureのApplication Insightsの出力に必要
            }
            return Task.FromResult(0);
        }

        public Task Warning(string message, Type type = null)
        {
            _logger.LogWarning($"Warning Message: {message}; Type: {type}");

            //NOTE: WebJobの場合は_telemetryClient.Flush() がないとApplicationInsightsに出力されない
            if (_telemetryClient != null)
            {
                _telemetryClient.Flush();  //AzureのApplication Insightsの出力に必要
            }
            return Task.FromResult(0);
        }

        public Task Error(string message, Exception exception = null, Type type = null)
        {
            _logger.LogError(exception, $"Error Message: {message}; Type: {type}");

            //NOTE: WebJobの場合は_telemetryClient.Flush() がないとApplicationInsightsに出力されない
            if (_telemetryClient != null)
            {
                _telemetryClient.Flush();  //AzureのApplication Insightsの出力に必要
            }
            return Task.FromResult(0);
        }
    }
}
