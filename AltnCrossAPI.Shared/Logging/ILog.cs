using NLog;
using System;

namespace AltnCrossAPI.Shared.Logging
{
    public interface ILogger
    {
        void Error(string message, Exception exception);

        void Info(string message);
    }
}