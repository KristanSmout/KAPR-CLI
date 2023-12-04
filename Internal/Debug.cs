using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPR_CLI.Internal
{
    internal class Debug
    {
        public static void debugRuntimeConfiguration()
        {
            var runtimeConfiguration = Program.runtimeConfiguration;
            runtimeConfiguration.logging = true;
            runtimeConfiguration.configurationFilePath = $"{Utilities.currentDirectory}\\Configuration.json";
            runtimeConfiguration.functionFileDirectory = $"{Utilities.currentDirectory}\\Functions";
            runtimeConfiguration.outputDirectory = $"{Utilities.currentDirectory}\\Output";
            runtimeConfiguration.forceScreenshot = true;
            runtimeConfiguration.timeout = 5;
            runtimeConfiguration.sendEmail = true;
            runtimeConfiguration.emailRecipientList = new List<string> { "ksmout@test.com", "test.test@test.cs" };
            runtimeConfiguration.userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0";
            runtimeConfiguration.headless = false;
            runtimeConfiguration.screenResolution = "1920x1080";
            runtimeConfiguration.actions = new List<string> { "Action1", "Action2", "Action3" };

            //Save to file
            string json = JsonConvert.SerializeObject(runtimeConfiguration, Formatting.Indented);
            File.WriteAllText($"{Utilities.currentDirectory}\\Actions.json", json);
        }

        public static void debugApplicationConfiguration()
        {
            var applicationConfiguration = Program.applicationConfiguration;
            applicationConfiguration.smtpServer = "smtp.test.com";
            applicationConfiguration.smtpPort = 587;
            applicationConfiguration.smtpSender = "test@test.com";
            applicationConfiguration.smtpUsername = "";
            applicationConfiguration.smtpPassword = "";

            //Save to file
            string json = JsonConvert.SerializeObject(applicationConfiguration, Formatting.Indented);
            File.WriteAllText($"{Utilities.currentDirectory}\\Configuration.json", json);

        }
    }
}
