using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace JonkerBudget.WorkerApi.Global
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private static readonly Logger logger = LogManager.GetLogger("Exception");

        public override void Log(ExceptionLoggerContext context)
        {
            if (context.ExceptionContext.ControllerContext != null)
            {
                //Make sure the controller inherit from base type BaseController;     
                //var controller = (ControllerBase)context.ExceptionContext.ControllerContext.Controller;
                //IRequestInfoProvider requestProvider = controller.RequestInfoProvider;

                //logger.Error(context.Exception, "Api Exception - {0} - {1}", requestProvider.Current.Username, requestProvider.Current.RequestId);
            }
            else
            {
                logger.Error(context.Exception, "Api Exception - {0} - {1}", "Anonymous", Guid.Empty.ToString());
            }
        }

        private string GetUserName()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            if (string.IsNullOrWhiteSpace(userName)) return "Anonymous";
            return userName;
        }

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            return base.LogAsync(context, cancellationToken);
        }

        public override bool ShouldLog(ExceptionLoggerContext context)

        {
            return true; //log by default
        }
    }
}