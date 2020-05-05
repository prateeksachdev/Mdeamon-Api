using NLog;
using System;

namespace AltnCrossAPI.Shared.Logging
{
    public class Logger : ILogger
    {
        private readonly NLog.Logger log = LogManager.GetCurrentClassLogger();

        public void Error(string message, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(message))
                message = exception.Message;

            log.Error(exception, message);
        }

        public void Info(string message)
        {
            log.Info(message);
        }
    }
}