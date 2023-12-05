using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KAPR_CLI.Internal
{
    internal class Configuration
    {
        public class ApplicationConfiguration
        {
            public string smtpServer { get; set; } = null;
            public int smtpPort { get; set; } = 587;
            public string smtpSender { get; set; } = null;
            public string smtpUsername { get; set; } = null;
            public string smtpPassword { get; set; } = null;
            public bool smtpEnableSSL { get; set; } = true;

        }
         
        public class RuntimeConfiguration
        {
            // KAPR Configuration
            public bool logging { get; set; } = false;
            public string? configurationFilePath { get; set; } = null;
            public string? functionFileDirectory { get; set; } = null;
            public string? outputDirectory { get; set; } = null;
            public bool forceScreenshot { get; set; } = false;
            public int? timeout { get; set; } = 500;

            // Optional Configuration
            public bool? sendEmail { get; set; } = false;
            public List<string>? emailRecipientList { get; set; } = null;


            // Selenium Configuration
            public string? userAgent { get; set; } = null;
            public bool headless { get; set; } = false;
            public string? screenResolution { get; set; } = "1920x1080";

            public List<string> actions { get; set; } = new List<string>();

        }
    }
}
