using System;

namespace Altn.Service.Plugin.Base.Interface
{
    public interface ILog
    {
        void Info(string message, long resourceId = 0, string resourceType = "general");
        void Error(string message, Exception exception, long resourceId = 0, string resourceType = "general");
    }
}