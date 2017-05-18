using DragonFire.Core.Exceptions;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Net;
using Newtonsoft.Json;
using DragonFire.Core.Request;
using JonkerBudget.WebApi.Controllers;
using System;

namespace JonkerBudget.WebApi.Global
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            bool handled = false;

            context = HandleSpecificException(context, out handled);

            var requestId = Guid.Empty.ToString();

            if (context.ExceptionContext.ControllerContext != null)
            {
                //Make sure the controller inherit from base type BaseController;     
                var controller = (ControllerBase)context.ExceptionContext.ControllerContext.Controller;
                IRequestInfoProvider requestProvider = controller.RequestInfoProvider;
                requestId = requestProvider.Current.RequestId;
            }

            if (!handled)                       
            {
                context.Result = new CustomErrorResult
                {
                    Request = context.ExceptionContext.Request,
                    Content = "An error has occurred on the server. Please contact support! : " + requestId
                };
            }
        }

        private ExceptionHandlerContext HandleSpecificException(ExceptionHandlerContext context, out bool handled)
        {
            handled = false;

            //Handle MessageException
            if (context.Exception is MessageException)
            {                
                context.Result = new CustomErrorResult(HttpStatusCode.BadRequest)
                {
                    Request = context.ExceptionContext.Request,
                    Content = context.Exception.Message
                };

                handled = true;
            }

            //Handle DomainValidationException
            if (context.Exception is DomainValidationException)
            {
                context.Result = new CustomErrorResult(HttpStatusCode.BadRequest)
                {
                    Request = context.ExceptionContext.Request,
                    Content = JsonConvert.SerializeObject(((DomainValidationException)context.Exception).RulesBroken)
                };

                handled = true;
            }            

            //Handle DbEntityValidationException
            if (context.Exception is DbEntityValidationException)
            {
                List<string> validationErrorMessages = new List<string>();

                foreach (var validationErrors in ((DbEntityValidationException)context.Exception).EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        validationErrorMessages.Add(string.Format("Property: {0}. Error: {1}", validationError.PropertyName, validationError.ErrorMessage));
                    }
                }

                context.Result = new CustomErrorResult
                {
                    Request = context.ExceptionContext.Request,
                    Content = "The following validation errors occurred: " + string.Join(" ", validationErrorMessages)
                };

                handled = true;
            }

            return context;
        }

        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            return base.HandleAsync(context, cancellationToken);         
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            //Handle all errors by default
            return true;
        }
    }    
}