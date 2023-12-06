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
        public static string outputDirectory = "";

        //KAPR Variables
        public static string baseURL = "";
        public static List<string> runInstructions = new List<string>();





        public static void Main(string[] args)
        {
            if (debugMode)
            {
                Debug.debugRuntimeConfiguration();

                //Useragent Regex
                Regex userAgentRegex = new Regex(@"-(-useragent|u) (.+?)\s(?=-)");
                Regex removeUserAgentRegex = new Regex(@"-(-useragent|u) .*-");

                //string debugargs = @"--smtpserver smtp.test.com --smtpport 58700 --smtpsender testsender --smtpusername testusername --smtppassword testpassword --smtpenablessl true --file F:\Development\Private-Software\KAPR-CLI\bin\Debug\net7.0\Actions.json --headless true --resolution 1920,1080 --useragent ""Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:89.0) Gecko/20100101 Firefox/89.0"" --outputdirectory F:\Development\Private-Software\KAPR-CLI\bin\Debug\net7.0\Output --timeout 5 --sendemail true --emailrecipientlist test@test.com --logging true";
                string debugargs = @"-v true";
                //

                Regex regex = new Regex(@"[^\s""]+|""(""([^""]*)""|([^""]*))+""");
                MatchCollection matches = regex.Matches(debugargs);

                string[] debugargsarray = new string[matches.Count];
                for (int i = 0; i < matches.Count; i++)
                {
                    debugargsarray[i] = matches[i].Value.Trim('"');
                }

                args = debugargsarray;


                Output.Debug("Debug mode enabled");
                Output.Debug("Application Directory: " + currentDirectory.FullName);
            }

            if (args.Length == 0)
            {
                Output.Error("No arguments provided\nRun the application with '-h' or '--help' for valid arguments.");
            }
            else
            {
                foreach (string arg in args)
                {
                    if (arg == "-r" || arg == "--resolution")
                    {
                        int index = Array.IndexOf(args, arg);
                        string resolution = args[index + 1];
                        args = args.Where((source, i) => i != index && i != index + 1).ToArray();
                        string[] resolutionArray = resolution.Split('x', ',');
                        string[] newargs = { arg, resolutionArray[0], resolutionArray[1] };
                        args = args.Concat(newargs).ToArray();
                        break;
                    }    
                }

                Arguments.parseArguments(args);
                Debug.ShowConfigruationandActions();
                Utilities.createOutputDirectory();
                Utilities.setupChrome();


            }



        }
    }
}