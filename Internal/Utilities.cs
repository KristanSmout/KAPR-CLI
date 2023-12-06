using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static KAPR_CLI.Internal.Output;
using static KAPR_CLI.Internal.WebHelper;
using System.Reflection;
using OpenQA.Selenium.Chrome;

namespace KAPR_CLI.Internal
{
    internal class Utilities
    {

        public static DirectoryInfo currentDirectory = new DirectoryInfo(AppContext.BaseDirectory);

        public static string getVersion()
        {

            Assembly? assembly = System.Reflection.Assembly.GetExecutingAssembly();
            string Fullname = assembly.FullName;
            string version = Fullname.Split(',')[1].Split('=')[1];
            return version;
        }

        public static string FixLocalReferencesInHtml(ref string htmlContent, string baseUrl)
        {
            Output.Debug("Fixing local references in HTML");
            htmlContent = Regex.Replace(htmlContent, @"(src|href)=['""](/[^'""]+)['""]", $"$1='{baseUrl}$2'");
            return htmlContent;
        }

        public static void sendEmail(Configuration.ApplicationConfiguration? applicationConfiguration = null, Configuration.RuntimeConfiguration runtimeConfiguration = null, string subject = "KAPR Error", string body = "An undefined error has occured", Attachment? attatchment = null)
        {
            SmtpClient smtpClient = new SmtpClient(applicationConfiguration.smtpServer)
            {
                Port = applicationConfiguration.smtpPort,
                Credentials = new System.Net.NetworkCredential(applicationConfiguration.smtpUsername, applicationConfiguration.smtpPassword),
                EnableSsl = applicationConfiguration.smtpEnableSSL,
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(applicationConfiguration.smtpSender),
                Subject = subject,
                Body = body,
            };

            if (attatchment != null)
            {
                mailMessage.Attachments.Add(attatchment);
            }

            foreach (string recipient in runtimeConfiguration.emailRecipientList)
            {
                mailMessage.To.Add(recipient);
            }

            smtpClient.Send(mailMessage);


        }

        public static void createOutputDirectory()
        {
            if (!Directory.Exists(Program.runtimeConfiguration.outputDirectory))
            {
                Output.Debug("Creating output directory");
                try
                {
                    Directory.CreateDirectory(Program.runtimeConfiguration.outputDirectory);
                }
                catch (Exception e)
                {
                    Output.Error("Failed to create output directory");
                    Output.Error(e.Message);
                    Environment.Exit(1);
                }
            }

            Program.outputDirectory = Directory.CreateDirectory($"{Program.runtimeConfiguration.outputDirectory}\\{DateTime.Now.ToString("dd-MM-yyyy h-mm-ss tt")}").ToString();
            Directory.CreateDirectory($"{Program.outputDirectory}\\screenshots");
            Directory.CreateDirectory($"{Program.outputDirectory}\\snapshots");
            Output.Debug($"Output directory created: {Program.outputDirectory}");

        }

        public static void setupChrome()
        {
            Output.Debug("Setting up Chrome");
            ChromeOptions chromeOptions = new ChromeOptions();
            
            

            if(Program.runtimeConfiguration.headless)
            {
                Output.Debug("Headless mode enabled");

            }

        }

    }
}
