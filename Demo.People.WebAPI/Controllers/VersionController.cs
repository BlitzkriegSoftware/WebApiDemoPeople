using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;

namespace Demo.People.WebAPI.Controllers
{
    /// <summary>
    /// Version of API
    /// </summary>
    [RoutePrefix("version")]
    public class VersionController : ApiController
    {
        /// <summary>
        /// Show the Version Information for the current API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route()]
        [ResponseType(typeof(Models.VersionInfo))]
        public IHttpActionResult Version()
        {
            var version = new Models.VersionInfo()
            {
                Build = Build(),
                Copyright = Copyright(),
                Major = Major(),
                Minor = Minor(),
                Private = Private()
            };
            return Ok(version);
        }

        private int Major()
        {
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileMajorPart;
        }

        private int Minor()
        {
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileMinorPart; 
        }

        private int Build()
        {
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileBuildPart;
        }

        private int Private()
        {
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FilePrivatePart;
        }

        private  string Copyright()
        {
            return System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LegalCopyright;
        }

    }
}
