using System.Collections.Generic;
using System.Reflection;

namespace DragonFire.Intercept
{
    public interface IDragonFireInterceptor
    {
        bool MustLog(MemberInfo methodInfo);
        void LogInvocation(Dictionary<string, string> audit);
    }
}
