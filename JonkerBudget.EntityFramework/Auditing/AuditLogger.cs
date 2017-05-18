using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace DragonFireTemplate.EntityFramework.Audit
{
    public class AuditLogger
    {
        //Todo - check for insert, update, delete
        internal static void LogQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            
        }

        internal static void LogReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (interceptionContext.TaskStatus == System.Threading.Tasks.TaskStatus.Created 
                && interceptionContext.Result.FieldCount > 0)
            {
                //Insert
            }
        }
    }
}
