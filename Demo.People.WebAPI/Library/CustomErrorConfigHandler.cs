using System.Web.Configuration;

namespace Demo.People.WebAPI.Library
{
    /// <summary>
    /// Reads the  web.config, system.web, customErrors section to see the mode
    /// </summary>
    public static class CustomErrorConfigHandler
    {
        /// <summary>
        /// If mode is OFF, this is true
        /// </summary>
        public static bool ShouldSupplyFullException
        {
            get
            {
                bool shouldShow = false;

                try
                {
                    CustomErrorsSection customErrors = (CustomErrorsSection)WebConfigurationManager.OpenWebConfiguration("/").GetSection("system.web/customErrors");
                    CustomErrorsMode mode = customErrors.Mode;
                    if (mode == CustomErrorsMode.Off) shouldShow = true;
                }
                catch
                {
                    shouldShow = false;
                }

                return shouldShow;
            }
        }

    }
}