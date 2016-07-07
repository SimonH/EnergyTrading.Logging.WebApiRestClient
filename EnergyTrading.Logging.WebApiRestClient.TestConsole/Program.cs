using System;
using System.Reflection;

namespace EnergyTrading.Logging.WebApiRestClient.TestConsole
{
    class Program
    {
        static void Main()
        {
            var factory = new LoggerFactory("http://localhost:27536");
            Logging.LoggerFactory.SetProvider(() => factory);
            Logging.LoggerFactory.Initialize();
            var logger = Logging.LoggerFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            var message = GetMessage();
            while (!string.Equals(message, "quit", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    logger.Debug(message);
                }
                else
                {
                    System.Console.WriteLine("message was empty.. Nothing sent");
                }
                message = GetMessage();
            }
        }

        public static string GetMessage()
        {
            System.Console.WriteLine("Type a log message (or 'quit' to exit) and Press Enter");
            return System.Console.ReadLine();
        }
    }
}
