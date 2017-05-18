using DragonFire.Core.Logging;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;

namespace DragonFireTemplate.Logging.Domain
{
    public class DomainServiceLogger : IDomainServiceLogger
    {
        private static readonly Logger logger = LogManager.GetLogger("Domain");

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message, Exception ex)
        {
            logger.Error(ex, message, null);
        }

        public void Fatal(string message, Exception ex)
        {
            logger.Fatal(ex, message, null);
        }

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Trace(string message, Exception ex)
        {
            logger.Trace(ex, message, null);
        }

        public void Warning(string message, Exception ex)
        {
            logger.Warn(ex, message, null);
        }

        public void LogProperties(Dictionary<string, string> properties)
        {
            logger.Info(JsonConvert.SerializeObject(properties));
        }
    }
}

//Trace - very detailed logs, which may include high-volume information such as protocol payloads.This log level is typically only enabled during development
//Debug - debugging information, less detailed than trace, typically not enabled in production environment.
//Info - information messages, which are normally enabled in production environment
//Warn - warning messages, typically for non-critical issues, which can be recovered or which are temporary failures
//Error - error messages - most of the time these are Exceptions
//Fatal - very serious errors!
