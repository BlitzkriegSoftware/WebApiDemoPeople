using System;
using System.Web.Http.ExceptionHandling;
using System.Net;

namespace Demo.People.WebAPI.Library
{
    /// <summary>
    /// Global Error Handling -> Provide an exception handler
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="context">Call context</param>
        public override void Handle(ExceptionHandlerContext context)
        {
            var showDetails = Library.CustomErrorConfigHandler.ShouldSupplyFullException;

            Exception ex = null;
            string content = string.Empty;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            if ((context.ExceptionContext != null) && (context.ExceptionContext.Exception != null))
            {
                ex = context.ExceptionContext.Exception;
            }
            else
            {
                if (context.Exception != null)
                {
                    ex = context.Exception;
                }
            }

            if (ex != null)
            {
                if (showDetails) content = ex.ToString();
                else content = ex.Message;

                // TODO: Add in more exceptions and the result codes you need to handle

                TypeSwitch.Do(ex,
                    TypeSwitch.Case<ArgumentException>(() => { statusCode = HttpStatusCode.BadRequest; }),
                    TypeSwitch.Case<ArgumentNullException>(() => { statusCode = HttpStatusCode.BadRequest; }),
                    TypeSwitch.Case<ArgumentOutOfRangeException>(() => { statusCode = HttpStatusCode.BadRequest; }),
                    TypeSwitch.Case<ValidationException>(() => { statusCode = HttpStatusCode.BadRequest; content = ((ValidationException)ex).ValidationText("\n"); })
                );
            }

            context.Result = new TextPlainErrorResult()
            {
                StatusCode = statusCode,
                Content = content,
                Request = context.ExceptionContext.Request
            };
        }

        /// <summary>
        /// Should handle the exception?
        /// </summary>
        /// <param name="context">ExceptionHandlerContext</param>
        /// <returns>Always true</returns>
        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

    }
}