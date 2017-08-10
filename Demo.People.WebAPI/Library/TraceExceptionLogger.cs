using System.Diagnostics;
using System.Web.Http.ExceptionHandling;

namespace Demo.People.WebAPI.Library
{
    /// <summary>
    /// Trace exception logger
    /// </summary>
    public class TraceExceptionLogger : ExceptionLogger
    {
        /// <summary>
        /// Log method called on error
        /// </summary>
        /// <param name="context"></param>
        public override void Log(ExceptionLoggerContext context)
        {
            if (context.ExceptionContext != null)
            {
                Trace.TraceError(context.ExceptionContext.Exception.ToString());
            }
            else
            {
                if (context.Exception != null)
                {
                    Trace.TraceError(context.Exception.ToString());
                }
            }
        }

        /// <summary>
        /// Should log?
        /// </summary>
        /// <param name="context">ExceptionLoggerContext</param>
        /// <returns>Always true</returns>
        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return true;
        }
    }

}