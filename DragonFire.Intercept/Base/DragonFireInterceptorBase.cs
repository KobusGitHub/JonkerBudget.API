using DragonFire.Core.Auditing;
using NLog;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace DragonFire.Intercept
{
    public class DragonFireInterceptorBase : IDragonFireInterceptor
    {
        private static readonly Logger logger = LogManager.GetLogger("log");

        public void LogInvocation(Dictionary<string, string> audit)
        {
            logger.Info(CreateMethodAuditEntry(audit));
        }

        private string CreateMethodAuditEntry(Dictionary<string, string> audit)
        {
            string entry = string.Empty;
                        
            if (audit != null)
            {
                foreach (var key in audit)
                {
                    entry += string.Format("{0}:{1}", key.Key, key.Value);
                }
            }

            return entry;
        }

        public bool MustLog(MemberInfo methodInfo)
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(DontAudit), false);
            if (attrs.Length > 0)
            {
                return false;
            }

            attrs = methodInfo.GetCustomAttributes(typeof(Audit), false);

            if (attrs.Length > 0)
            {
                return true;
            }

            attrs = methodInfo.DeclaringType.GetCustomAttributes(typeof(Audit), false);

            if (attrs.Length > 0)
            {
                return true;
            }

            return false;
        }
    }
}
