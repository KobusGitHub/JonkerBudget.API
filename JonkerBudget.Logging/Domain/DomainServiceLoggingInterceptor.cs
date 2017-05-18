using Castle.DynamicProxy;
using DragonFire.Core.Auditing;
using DragonFire.Core.Logging;
using DragonFireTemplate.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DragonFireTemplate.Logging.Domain
{
    public class DomainServiceLoggingInterceptor : IInterceptor
    {
        IDomainServiceLogger logger;

        public DomainServiceLoggingInterceptor(IDomainServiceLogger logger)
        {
            this.logger = logger;
        }

        public virtual void Intercept(IInvocation invocation)
        {
            var proxy = invocation.Proxy as IProxyTargetAccessor;
            DomainService accessor = (DomainService)proxy.DynProxyGetTarget();
            var requestInfoProvider = accessor.RequestInfoProvider;

            if (!MustLog(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }
            
            var audit = new Dictionary<string, string>();

            audit.Add("Service", invocation.TargetType.Name);
            audit.Add("Method", invocation.Method.Name);
            audit.Add("Parameters", string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));
            audit.Add("TimeStampUtc", DateTime.UtcNow.ToString());
            audit.Add("Username", requestInfoProvider.Current.Username);
            audit.Add("RequestId", requestInfoProvider.Current.ToString());

            LogInvocation(audit);

            invocation.Proceed();
        }

        private void LogInvocation(Dictionary<string, string> audit)
        {
            try
            {
                logger.LogProperties(audit);
            }
            catch (Exception ex)
            {
                //Log with internal logging mechanism if logging fails
                CoreLogger.LogException(ex);
            }
        }

        private bool MustLog(MemberInfo methodInfo)
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