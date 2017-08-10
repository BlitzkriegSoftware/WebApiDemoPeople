using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.People.WebAPI.Models
{
    /// <summary>
    /// Version Information
    /// </summary>
    public class VersionInfo
    {
        /// <summary>
        /// Major number (1st)
        /// </summary>
        public int Major { get; set; }
        /// <summary>
        /// Minor number (2nd)
        /// </summary>
        public int Minor { get; set; }
        /// <summary>
        /// Build (3rd)
        /// </summary>
        public int Build { get; set; }
        /// <summary>
        /// Private (4th)
        /// </summary>
        public int Private { get; set; }

        /// <summary>
        /// Dotted Version
        /// </summary>
        public string Version
        {
            get
            {
                return string.Format("{0}.{1}.{2}.{3}", this.Major, this.Minor, this.Build, this.Private);
            }
        }

        /// <summary>
        /// Copyright
        /// </summary>
        public string Copyright { get; set; }
    }
}