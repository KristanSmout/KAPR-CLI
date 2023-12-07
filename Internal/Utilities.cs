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

        public static void sendEmail(string subject = "KAPR Error", string body = "An undefined error has occured", Attachment? attatchment = null)
        {
            SmtpClient smtpClient = new SmtpClient(Program.applicationConfiguration.smtpServer)
            {
                Port = Program.applicationConfiguration.smtpPort,
                Credentials = new System.Net.NetworkCredential(Program.applicationConfiguration.smtpUsername, Program.applicationConfiguration.smtpPassword),
                EnableSsl = Program.applicationConfiguration.smtpEnableSSL,
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(Program.applicationConfiguration.smtpSender),
                Subject = subject,
                Body = body,
            };

            if (attatchment != null)
            {
                mailMessage.Attachments.Add(attatchment);
            }

            foreach (string recipient in Program.runtimeConfiguration.emailRecipientList)
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

            //Screen Resolution
             chromeOptions.AddArgument($"window-size={Program.runtimeConfiguration.screenResolution[0]},{Program.runtimeConfiguration.screenResolution[1]}");

            //User Agent
            chromeOptions.AddArgument($"--user-agent={Program.runtimeConfiguration.userAgent}");

            //Headless
            if(!Program.runtimeConfiguration.visible)
            {
                chromeOptions.AddArgument("--headless");
            }

            Program.driver = new ChromeDriver(chromeOptions);
            using (Program.driver)
            {
                Program.session = Program.driver.GetDevToolsSession();

                foreach(string action in Program.runtimeConfiguration.actions)
                {
                    Actions.ExecuteCommand(action);
                }

            }



        }


        //Chrome Actions
        public static IWebElement element(Actions.LocatorType type, string value)
        {
            IWebElement element = null;

            try
            {

                switch (type)
                {
                    case (Actions.LocatorType)LocatorType.Id:
                        element = Program.driver.FindElement(By.Id(value));
                        break;

                    case (Actions.LocatorType)LocatorType.CssSelector:
                        element = Program.driver.FindElement(By.CssSelector(value));
                        break;

                    case (Actions.LocatorType)LocatorType.XPath:
                        element = Program.driver.FindElement(By.XPath(value));
                        break;

                    case (Actions.LocatorType)LocatorType.Text:
                        element = Program.driver.FindElement(By.XPath($"//*[text()='{value}']"));
                        break;

                    case (Actions.LocatorType)LocatorType.LinkText:
                        element = Program.driver.FindElement(By.LinkText(value));
                        break;

                    case (Actions.LocatorType)LocatorType.PartialLinkText:
                        element = Program.driver.FindElement(By.PartialLinkText(value));
                        break;

                    case (Actions.LocatorType)LocatorType.Name:
                        element = Program.driver.FindElement(By.Name(value));
                        break;

                    case (Actions.LocatorType)LocatorType.TagName:
                        element = Program.driver.FindElement(By.TagName(value));
                        break;

                    case (Actions.LocatorType)LocatorType.JavaScript:
                        IJavaScriptExecutor js = (IJavaScriptExecutor)Program.driver;
                        element = (IWebElement)js.ExecuteScript($"return document.querySelector('{value}')");
                        break;

                    default:
                        Output.Debug($"Locator Type {type} not found: {value}");
                        return null;
                }
            }
            catch (NoSuchElementException e)
            {
                return null;
            }

            return element;
        }


    }
}
