using System;
using System.Collections.Generic;
using NLog;
using DragonFire.Core.Logging;

namespace JonkerBudget.Application.Logging
{
    public class ApplicationServiceLogger : IApplicationServiceLogger
    {
        Logger log = LogManager.GetLogger("log");

        public void Debug(string message)
        {
            log.Debug(message);
        }

        public void Error(string message, Exception ex)
        {
            log.Error(ex, message);
        }

        public void Fatal(string message, Exception ex)
        {
            log.Fatal(ex, message);
        }

        public void Information(string message)
        {
            log.Info(message);
        }

        public void LogProperties(Dictionary<string, string> properties)
        {
            log.Info(properties);
        }

        public void Trace(string message, Exception ex)
        {
            log.Trace(ex, message);
        }

        public void Warning(string message, Exception ex)
        {
            log.Warn(ex, message);
        }
    }
}
