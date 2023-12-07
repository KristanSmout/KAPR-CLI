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
            runtimeConfiguration.emailRecipientList = new List<string> { "KAPR@kristansmout.co.uk", "tangofanta37@gmail.com" };
            runtimeConfiguration.userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0";
            runtimeConfiguration.visible = false;
            runtimeConfiguration.screenResolution = new string[] { "1920", "1080" };
            runtimeConfiguration.actions = new List<string> { "Action1", "Action2", "Action3" };

            //Save to file
            string json = JsonConvert.SerializeObject(runtimeConfiguration, Formatting.Indented);
            File.WriteAllText($"{Utilities.currentDirectory}\\Actions.json", json);
        }

        public static void debugApplicationConfiguration()
        {
            var applicationConfiguration = Program.applicationConfiguration;
            applicationConfiguration.smtpServer = "smtp.gmail.com";
            applicationConfiguration.smtpPort = 587;
            applicationConfiguration.smtpSender = "kristanrsmout@gmail.com";
            applicationConfiguration.smtpUsername = "kristanrsmout@gmail.com";
            applicationConfiguration.smtpPassword = "rbduysnkfkdpopnj";

            //Save to file
            string json = JsonConvert.SerializeObject(applicationConfiguration, Formatting.Indented);
            File.WriteAllText($"{Utilities.currentDirectory}\\Configuration.json", json);

        }

        public static void ShowConfigruationandActions()
        {
            Output.Log("Runtime Configuration");
            Output.Log("Logging: " + Program.runtimeConfiguration.logging);
            Output.Log("Configuration File Path: " + Program.runtimeConfiguration.configurationFilePath);
            Output.Log("Function File Directory: " + Program.runtimeConfiguration.functionFileDirectory);
            Output.Log("Output Directory: " + Program.runtimeConfiguration.outputDirectory);
            Output.Log("Force Screenshot: " + Program.runtimeConfiguration.forceScreenshot);
            Output.Log("Timeout: " + Program.runtimeConfiguration.timeout);
            Output.Log("Send Email: " + Program.runtimeConfiguration.sendEmail);
            Output.Log("Email Recipient List: ");
            foreach (string email in Program.runtimeConfiguration.emailRecipientList)
            {
                Output.Log("    " + email);
            }
            Output.Log("User Agent: " + Program.runtimeConfiguration.userAgent);
            Output.Log("Visible: " + Program.runtimeConfiguration.visible);
            Output.Log("Screen Resolution: " + Program.runtimeConfiguration.screenResolution);
            Output.Log("Actions: ");
            foreach (string action in Program.runtimeConfiguration.actions)
            {
                Output.Log("    " + action);
            }

            Output.Log("Application Configuration");
            Output.Log("SMTP Server: " + Program.applicationConfiguration.smtpServer);
            Output.Log("SMTP Port: " + Program.applicationConfiguration.smtpPort);
            Output.Log("SMTP Sender: " + Program.applicationConfiguration.smtpSender);
            Output.Log("SMTP Username: " + Program.applicationConfiguration.smtpUsername);
            Output.Log("SMTP Password: " + Program.applicationConfiguration.smtpPassword);
            Output.Log("SMTP Enable SSL: " + Program.applicationConfiguration.smtpEnableSSL);
        }
    }
}
