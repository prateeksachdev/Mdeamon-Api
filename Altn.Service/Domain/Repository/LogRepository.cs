using Dapper;
using NLog;
using System;
using Altn.Service.Plugin.Base.Interface;

namespace Altn.Service.Domain.Repository
{
    public class LogRepository : BaseRepository, ILog
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public void Error(string message, Exception exception, long resourceId = 0, string resourceType = "general")
        {
            if (string.IsNullOrWhiteSpace(message))
                message = exception.Message;

            log.Error(exception, resourceType + ":" + ":" + resourceId + ":" + message);
        }

        public void Info(string message, long resourceId = 0, string resourceType = "general")
        {
            log.Info(resourceType + ":" + ":" + resourceId + ":" + message);
        }
    }
}