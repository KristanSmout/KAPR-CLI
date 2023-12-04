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

        public static Assembly? assembly = System.Reflection.Assembly.GetExecutingAssembly();

        public class ApplicationConfiguration
        {
            public string smtpServer { get; set; } = "smtp.gmail.com";
            public int smtpPort { get; set; } = 587;
            public string smtpSender { get; set; } = null;
            public string smtpUsername { get; set; } = null;
            public string smtpPassword { get; set; } = null;
            public bool smtpEnableSSL { get; set; } = true;

        }

        public class RuntimeConfiguration
        {
            // Technical Configuration
            public static DirectoryInfo currentDirectory = new DirectoryInfo(AppContext.BaseDirectory);
            public string version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().ToString();

            // KAPR Configuration
            public bool logging { get; set; } = false;
            public string? configurationFilePath { get; set; } = null;
            public string? variableFileDirectory { get; set; } = null;
            public string? outputDirectory { get; set; } = null;
            public bool forceScreenshot { get; set; } = false;
            public int? timeout { get; set; } = 5;

            // Optional Configuration
            public bool? sendEmail { get; set; } = null;
            public string? emailRecipientList { get; set; } = null;


            // Selenium Configuration
            public string? userAgent { get; set; } = null;
            public bool headless { get; set; } = false;
            public string? screenResolution { get; set; } = null;
        }
    }
}
