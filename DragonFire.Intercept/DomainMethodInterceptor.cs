using Castle.DynamicProxy;
using DragonFire.Intercept;
using JonkerBudget.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonFireTemplate.Intercept
{
    public class DomainServiceMethodInterceptor : DragonFireInterceptorBase, IInterceptor
    {
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
    }
}