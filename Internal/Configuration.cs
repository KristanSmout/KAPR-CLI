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
            public List<string>? emailRecipientList { get; set; } = new List<string>();


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

        public static void createActionConfiguration(string[] arguments)
        {
            string runtimeConfigurationFilePath = null;
            RuntimeConfiguration config = new RuntimeConfiguration();

            if(arguments.Contains("-f") || arguments.Contains("--function"))
            {
                runtimeConfigurationFilePath = arguments[Array.IndexOf(arguments, "-f") + 1];
            }
            else if (File.Exists($"{Utilities.currentDirectory}\\Actions.json"))
            {
                runtimeConfigurationFilePath = $"{Utilities.currentDirectory}\\Actions.json";
            }
            else
            {
                Output.Warning("No action configuration used");
            }

            if (runtimeConfigurationFilePath != null)
            {
                try
                {
                    if (File.Exists(runtimeConfigurationFilePath))
                    {
                        Program.runtimeConfiguration = JsonConvert.DeserializeObject<RuntimeConfiguration>(File.ReadAllText(runtimeConfigurationFilePath))!;
                    }
                }
                catch (Exception e)
                {
                    Output.Error($"Failed to load runtime configuration: {e.Message}");
                    Environment.Exit(1);
                }
            }
            else
            {
                Program.runtimeConfiguration = new RuntimeConfiguration();
            }

            // Check Overrides
            for (int i = 0; i < arguments.Length; i++)
            {
                switch (arguments[i])
                {
                    case "--logging":
                        Program.runtimeConfiguration!.logging = bool.Parse(arguments[i + 1]);
                        break;
                    case "--configurationfilepath":
                        Program.runtimeConfiguration!.configurationFilePath = arguments[i + 1];
                        break;
                    case "--functionfiledirectory":
                        Program.runtimeConfiguration!.functionFileDirectory = arguments[i + 1];
                        break;
                    case "--outputdirectory":
                        Program.runtimeConfiguration!.outputDirectory = arguments[i + 1];
                        break;
                    case "--forcescreenshot":
                        Program.runtimeConfiguration!.forceScreenshot = bool.Parse(arguments[i + 1]);
                        break;
                    case "--timeout":
                        Program.runtimeConfiguration!.timeout = int.Parse(arguments[i + 1]);
                        break;
                    case "--sendemail":
                        Program.runtimeConfiguration!.sendEmail = bool.Parse(arguments[i + 1]);
                        break;
                    case "--emailrecipientlist":
                        Program.runtimeConfiguration!.emailRecipientList = arguments[i + 1].Split(",").ToList();
                        break;
                    case "--useragent":
                        Program.runtimeConfiguration!.userAgent = arguments[i + 1];
                        break;
                    case "--headless":
                        Program.runtimeConfiguration!.headless = bool.Parse(arguments[i + 1]);
                        break;
                    case "--screenresolution":
                        Program.runtimeConfiguration!.screenResolution = arguments[i + 1];
                        break;
                    case "--actions":
                        Program.runtimeConfiguration!.actions = arguments[i + 1].Split(",").ToList();
                        break;
                }
                i++;
            }

        }


    }
}
