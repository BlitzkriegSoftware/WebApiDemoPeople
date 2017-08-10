using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace Demo.People.WebAPI
{
    /// <summary>
    /// WebAPI Configuration
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // ------------------------------------------------------------------
            // Enable CORS support
            // http://www.asp.net/web-api/overview/security/enabling-cross-origin-requests-in-web-api
            // ------------------------------------------------------------------
            string origins = "*"; // Domains that are allowed to connect
            string headers = "*"; // header allowed
            string methods = "GET, POST, DELETE"; // Methods allowed like GET, POST, etc.
            config.EnableCors(new EnableCorsAttribute(origins, headers, methods));

            // ------------------------------------------------------------------
            // Use this to enable processing per controller and method
            // [Route*()] attributes
            // ------------------------------------------------------------------
            config.MapHttpAttributeRoutes();

            // REPLACE the default ERROR handler
            config.Services.Replace(typeof(IExceptionHandler), new Library.GlobalExceptionHandler());
            // ADD the default LOGGER
            config.Services.Add(typeof(IExceptionLogger), new Library.TraceExceptionLogger());
        }
    }
}
