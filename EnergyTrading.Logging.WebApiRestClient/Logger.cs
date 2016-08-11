using System;
using System.Collections.Generic;
using EnergyTrading.Contracts.Logging;

namespace EnergyTrading.Logging.WebApiRestClient
{
    public class Logger : ILogger
    {
        private readonly LoggerGateway loggerGateway;
        private readonly string type;
        public Logger(string baseUri, string type)
        {
            if (string.IsNullOrWhiteSpace(baseUri))
            {
                throw new ArgumentOutOfRangeException(nameof(baseUri));
            }
            loggerGateway = new LoggerGateway(baseUri);
            this.type = type;
        }

        private void SendLogMessage(string level, string message, Exception exception, params object[] parameters)
        {
            // do we need to make sure that the parameters are all serializable?
            var messageContract = new LogMessage
            {
                Level = level,
                CreatingType = this.type,
                Exception = LogMessageException.FromException(exception),
                Message = message,
                Params = FixParameterExceptions(parameters)
            };
            // send the message 
            loggerGateway.PostAsync(messageContract).Wait(); // TODO : - Do we need to wait here? probably not but it probably doesn't hurt either (see if performance becomes an issue or not and/or whether we need to check for a successful result)
        }

        public void Debug(string message)
        {
            SendLogMessage("Debug", message, null);
        }

        private static object[] FixParameterExceptions(object[] parameters)
        {
            if (parameters == null)
            {
                return null;
            }
            var list = new List<object>();
            foreach (var obj in parameters)
            {
                var exc = obj as Exception;
                if (exc != null && !(obj is LogMessageException))
                {
                    list.Add(LogMessageException.FromException(exc));
                }
                else
                {
                    list.Add(obj);
                }
            }
            return list.ToArray();
        }

        public void Debug(string message, Exception exception)
        {
            SendLogMessage("Debug", message, exception);
        }

        public void DebugFormat(string format, params object[] parameters)
        {
            SendLogMessage("Debug", format, null, parameters);
        }

        public void Info(string message)
        {
            SendLogMessage("Info", message, null);

        }
        public void Info(string message, Exception exception)
        {
            SendLogMessage("Info", message, exception);
        }

        public void InfoFormat(string format, params object[] parameters)
        {
            SendLogMessage("Info", format, null, parameters);

        }

        public void Warn(string message)
        {
            SendLogMessage("Warn", message, null);

        }

        public void Warn(string message, Exception exception)
        {
            SendLogMessage("Warn", message, exception);
        }

        public void WarnFormat(string format, params object[] parameters)
        {
            SendLogMessage("Info", format, null, parameters);

        }

        public void Error(string message)
        {
            SendLogMessage("Error", message, null);

        }

        public void Error(string message, Exception exception)
        {
            SendLogMessage("Error", message, exception);
        }

        public void ErrorFormat(string format, params object[] parameters)
        {
            SendLogMessage("Error", format, null, parameters);
        }

        public void Fatal(string message)
        {
            SendLogMessage("Fatal", message, null);
        }

        public void Fatal(string message, Exception exception)
        {
            SendLogMessage("Fatal", message, exception);

        }

        public void FatalFormat(string format, params object[] parameters)
        {
            SendLogMessage("Fatal", format, null, parameters);
        }

        public bool IsDebugEnabled => true;
        public bool IsInfoEnabled => true;
        public bool IsWarnEnabled => true;
        public bool IsErrorEnabled => true;
        public bool IsFatalEnabled => true;
    }
}