using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;

namespace Demo.People.WebAPI.Library
{
    /// <summary>
    /// No Cache Attribute for WebAPI
    /// </summary>
    public class NoCacheHeaderAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {

        /// <summary>
        /// CTOR
        /// </summary>
        public NoCacheHeaderAttribute() { }

        /// <summary>
        /// Actual Action
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            context.Response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(0),
                NoCache = true,
                MustRevalidate = true
            };

            base.OnActionExecuted(context);
        }
    }

}