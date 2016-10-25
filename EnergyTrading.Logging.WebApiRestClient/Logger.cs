using System;
using System.Threading;
using System.Threading.Tasks;
using EnergyTrading.Contracts.Logging;
using EnergyTrading.Extensions;

namespace EnergyTrading.Logging.WebApiRestClient
{
    public class Logger : ILogger
    {
        private readonly LoggerGateway loggerGateway;
        private readonly string type;
        private readonly ILogger exceptionLogger;
        public Logger(string baseUri, string type, ILogger exceptionLogger = null)
        {
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                throw new ArgumentOutOfRangeException(nameof(baseUri));
            }
            loggerGateway = new LoggerGateway(baseUri);
            this.type = type;
            this.exceptionLogger = exceptionLogger;
        }

        private Task SendLogMessage(string level, string message, Exception exception, params object[] parameters)
        {
            // do we need to make sure that the parameters are all serializable?
            var messageContract = new LogMessage
            {
                Level = level,
                CreatingType = this.type,
                Exception = LogMessageException.FromException(exception),
                Message = message,
                Params = parameters
            };
            // send the message 
            return loggerGateway.PostAsync(messageContract);
        }

        private void ContinueWithExceptionLoggerAction(Task task, string originalMessage)
        {
            if (task != null && exceptionLogger != null)
            {
                task.ContinueWith((t, o) => 
                {
                    exceptionLogger.Warn($"Error calling logging server : {t.Exception.Flatten().AllExceptionMessages()}, original message {originalMessage}");
                }, null, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        public void Debug(string message)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Debug", message, null), message);
        }

        public void Debug(string message, Exception exception)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Debug", message, exception), message);
        }

        public void DebugFormat(string format, params object[] parameters)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Debug", format, null, parameters), string.Format(format, parameters));
        }

        public void Info(string message)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Info", message, null), message);

        }
        public void Info(string message, Exception exception)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Info", message, exception), message);
        }

        public void InfoFormat(string format, params object[] parameters)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Info", format, null, parameters), string.Format(format, parameters));
        }

        public void Warn(string message)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Warn", message, null), message);

        }

        public void Warn(string message, Exception exception)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Warn", message, exception), message);
        }

        public void WarnFormat(string format, params object[] parameters)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Info", format, null, parameters), string.Format(format, parameters));

        }

        public void Error(string message)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Error", message, null), message);

        }

        public void Error(string message, Exception exception)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Error", message, exception), message);
        }

        public void ErrorFormat(string format, params object[] parameters)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Error", format, null, parameters), string.Format(format, parameters));
        }

        public void Fatal(string message)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Fatal", message, null), message);
        }

        public void Fatal(string message, Exception exception)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Fatal", message, exception), message);

        }

        public void FatalFormat(string format, params object[] parameters)
        {
            ContinueWithExceptionLoggerAction(SendLogMessage("Fatal", format, null, parameters), string.Format(format, parameters));
        }

        public bool IsDebugEnabled => true;
        public bool IsInfoEnabled => true;
        public bool IsWarnEnabled => true;
        public bool IsErrorEnabled => true;
        public bool IsFatalEnabled => true;
    }
}