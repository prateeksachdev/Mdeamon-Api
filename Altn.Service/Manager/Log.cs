using System;
using Altn.Service.Plugin.Base.Interface;
using Altn.Service.Domain.Repository;

namespace Altn.Service.Manager
{
    public class Log : ILog
    {
        public void Error(string message, Exception exception, long resourceId = 0, string resourceType = "general")
        {
            new LogRepository().Error(message, exception, resourceId, resourceType);
        }

        public void Info(string message, long resourceId = 0, string resourceType = "general")
        {
            new LogRepository().Info(message, resourceId, resourceType);
        }
    }
}