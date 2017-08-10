using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace Demo.People.WebAPI.Controllers
{
    /// <summary>
    /// Error Simulator Controller
    /// </summary>
    [RoutePrefix("errors")]
    public class ErrorSimulatorController : ApiController
    {

        /// <summary>
        /// Simulate Error: 400 with our new validator exception
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("error400")]
        [ResponseType(typeof(string))]
        public IHttpActionResult Error400()
        {
            string result = "Oh, no, error expected!";

            if (!string.IsNullOrWhiteSpace(result)) throw new Library.ValidationException(
                "Oh, no Data!",
                "Error400",
                new List<string>() { "Validation Error 1", "Validation Error 2" }
             );

            return Ok(result);
        }

        /// <summary>
        /// Simulate: Error 500 (Invalid Operation Exception)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("error500")]
        [ResponseType(typeof(string))]
        public IHttpActionResult Error500()
        {
            string result = "Oh, no, error expected!";

            if (!string.IsNullOrWhiteSpace(result)) throw new InvalidOperationException("This was a bad thing to do");

            return Ok(result);
        }

    }
}
