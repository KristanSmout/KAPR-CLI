using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        public static void createConfiguration(string[] arguments)
        {
            string applicationConfigurationFilePath = null;
            ApplicationConfiguration config = new ApplicationConfiguration();


            if (arguments.Contains("-c") || arguments.Contains("--config"))
            {
                applicationConfigurationFilePath = arguments[Array.IndexOf(arguments, "-c") + 1];
            }
            else if (File.Exists($"{Utilities.currentDirectory}\\Configuration.json"))
            {
                applicationConfigurationFilePath = $"{Utilities.currentDirectory}\\Configuration.json";
            }
            else
            {
                Output.Warning("No configuration used");
            }


            if (applicationConfigurationFilePath != null)
            {
                try
                {
                    if (File.Exists(applicationConfigurationFilePath))
                    {
                        Program.applicationConfiguration = JsonConvert.DeserializeObject<ApplicationConfiguration>(File.ReadAllText(applicationConfigurationFilePath))!;
                    }
                }
                catch (Exception e)
                {
                    Output.Error($"Failed to load application configuration: {e.Message}");
                    Environment.Exit(1);
                }
            }
            else
            {
                Program.applicationConfiguration = new ApplicationConfiguration();
            }

            // Check Overrides
            for (int i = 0; i < arguments.Length; i++)
            {
                switch (arguments[i])
                {
                    case "--smtpserver":
                        Program.applicationConfiguration!.smtpServer = arguments[i + 1];
                        break;
                    case "--smtpport":
                        Program.applicationConfiguration!.smtpPort = int.Parse(arguments[i + 1]);
                        break;
                    case "--smtpsender":
                        Program.applicationConfiguration!.smtpSender = arguments[i + 1];
                        break;
                    case "--smtpusername":
                        Program.applicationConfiguration!.smtpUsername = arguments[i + 1];
                        break;
                    case "--smtppassword":
                        Program.applicationConfiguration!.smtpPassword = arguments[i + 1];
                        break;
                    case "--smtpenablessl":
                        Program.applicationConfiguration!.smtpEnableSSL = bool.Parse(arguments[i + 1]);
                        break;
                }
                i++;
            }
        }

    }
}
