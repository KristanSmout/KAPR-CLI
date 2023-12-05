using System;
using System.Reflection;
using System.Text.RegularExpressions;
using KAPR_CLI.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace KAPR_CLI
{
    internal class Program
    {
        // Configuration Variables
        public static bool debugMode = true;
        public static bool verboseMode = false;

        //Selenium Variables
        public static ChromeDriver driver;
        public static DevToolsSession session;

        //Runtime Variables
        public static DirectoryInfo currentDirectory = new DirectoryInfo(AppContext.BaseDirectory);
        public static Configuration.RuntimeConfiguration runtimeConfiguration = new Configuration.RuntimeConfiguration();
        public static Configuration.ApplicationConfiguration applicationConfiguration = new Configuration.ApplicationConfiguration();

        //KAPR Variables
        public static string baseURL = "google.co.uk";
        public static List<string> runInstructions = new List<string>();





        public static void Main(string[] args)
        {
            if (debugMode)
            {
                //Debug.debugRuntimeConfiguration();
                //Debug.debugApplicationConfiguration();
                string debugargs = @"--smtpserver smtp.test.com --smtpport 58700 --smtpsender testsender --smtpusername testusername --smtppassword testpassword --smtpenablessl true --file F:\Development\Private-Software\KAPR-CLI\bin\Debug\net7.0\Actions.json --headless true --screenresolution 1920x1080 --useragent Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0 --outputdirectory F:\Development\Private-Software\KAPR-CLI\bin\Debug\net7.0\Output --timeout 5 --force true --sendemail true --emailrecipientlist test@test.com --logging true";
                
                //Useragent Regex
                Regex userAgentRegex = new Regex(@"-(useragent|u) (.+?)\s(?=-)");
                Regex removeUserAgentRegex = new Regex(@"-(useragent|u) .*-");


                Output.Debug("Debug mode enabled");
                Output.Debug("Application Directory: " + currentDirectory.FullName);
            }

            if (args.Length == 0)
            {
                Output.Error("No arguments provided\nRun the application with '-h' or '--help' for valid arguments.");
            }
            else
            {
                Arguments.parseArguments(args);
                Debug.ShowConfigruationandActions();
            }

            

        }
    }
}