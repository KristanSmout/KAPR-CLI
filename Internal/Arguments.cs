﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static KAPR_CLI.Internal.Configuration;

namespace KAPR_CLI.Internal
{
    internal class Arguments
    {
        public static List<ArgumentDetails> argumentList = new List<ArgumentDetails>
        {
            new ArgumentDetails { Arguments = "-h, --help", Description = "Displays this help message" },
            new ArgumentDetails { Arguments = "-c, --config", Description = "KAPR configuration filepath" },
            new ArgumentDetails { Arguments = "-f, --file", Description = "KARP action filepath" },

            //Runtime Overrides
            new ArgumentDetails { Arguments = "-l, --logging", Description = "Logging" },
            new ArgumentDetails { Arguments = "-o, --output", Description = "Output Directory" },
            new ArgumentDetails { Arguments = "-s, --screenshot", Description = "Force Screenshots" },
            new ArgumentDetails { Arguments = "-t, --timeout", Description = "Timeout" },
            new ArgumentDetails { Arguments = "-e, --email", Description = "Email Recipient(s)" },
            new ArgumentDetails { Arguments = "-u, --useragent", Description = "User Agent" },
            new ArgumentDetails { Arguments = "-h, --headless", Description = "Headless" },
            new ArgumentDetails { Arguments = "-r, --resolution", Description = "Screen Resolution (X,Y)" },

            new ArgumentDetails { Arguments = "", Description = "" },

            //Configuration Overrides
            new ArgumentDetails { Arguments = "--smtpserver", Description = "Override SMTP Server" },
            new ArgumentDetails { Arguments = "--smtpport", Description = "Override SMTP Port" },
            new ArgumentDetails { Arguments = "--smtpsender", Description = "Override SMTP Sender" },
            new ArgumentDetails { Arguments = "--smtpusername", Description = "Override SMTP Username" },
            new ArgumentDetails { Arguments = "--smtppassword", Description = "Override SMTP Password" },
            new ArgumentDetails { Arguments = "--smtpenablessl", Description = "Override SMTP Enable SSL" },



        };

        public class ArgumentDetails
        {
            public string Arguments { get; set; }
            public string Description { get; set; }
        }

        public static void parseArguments(string[] arguments)
        {
            if (arguments.Contains("-h") || arguments.Contains("--help"))
            {
                printArgumentsTable();
            }
            else if (arguments.Contains("-v") || arguments.Contains("--version"))
            {
                Output.Log($"KAPR CLI {Utilities.getVersion()}");
            }
            else if (arguments.Contains("-a") || arguments.Contains("--actions"))
            {
                Actions.PrintActionTable();
            }
            else
            {
                createConfiguration(arguments);
            }
        }

        public static void printArgumentsTable()
        {
            int maxArgumentsLength = Math.Max("Arguments".Length, argumentList.Max(a => a.Arguments.Length));
            int maxDescriptionLength = Math.Max("Description".Length, argumentList.Max(a => a.Description.Length));

            Console.WriteLine($"| {"Arguments".PadRight(maxArgumentsLength)} | {"Description".PadRight(maxDescriptionLength)} |");


            Console.WriteLine($"|{new string('-', maxArgumentsLength + 2)}|{new string('-', maxDescriptionLength + 2)}|");

            foreach (var arg in argumentList)
            {
                Console.WriteLine($"| {arg.Arguments.PadRight(maxArgumentsLength)} | {arg.Description.PadRight(maxDescriptionLength)} |");
            }
        }

        public static void createConfiguration(string[] arguments)
        {
            string runtimeConfigurationFilePath = null;
            RuntimeConfiguration config = new RuntimeConfiguration();


            if(arguments.Contains("-c") || arguments.Contains("--config"))
            {
                config.configurationFilePath = arguments[Array.IndexOf(arguments, "-c") + 1];
            }
            else
            {
                if(File.Exists("kapr.json"))
                {
                    config.configurationFilePath = $"{Utilities.currentDirectory}\\Configuration.json";
                }
                else
                {
                    // Check Overrides


                    Output.Warning("No configuration file provided");
                }
            }

            Program.runtimeConfiguration = JsonConvert.DeserializeObject<RuntimeConfiguration>(File.ReadAllText(runtimeConfigurationFilePath));





            for (int i = 0; i < arguments.Length; i++)
            {
                string arg = arguments[i];
                switch (arg.ToLower())
                {
                    case "-c":
                    case "--config":
                        ApplicationConfiguration applicationConfiguration = JsonConvert.DeserializeObject<ApplicationConfiguration>(File.ReadAllText(arguments[i + 1]));
                        if (applicationConfiguration != null)
                        {
                            Output.Log("Application configuration loaded");
                            Program.applicationConfiguration = applicationConfiguration;
                        }
                        else
                        {
                            Output.Error("Application configuration failed to load");
                            Environment.Exit(1);
                        }
                        break;

                    case "-f":
                    case "--file":
                        RuntimeConfiguration runtimeConfiguration = JsonConvert.DeserializeObject<RuntimeConfiguration>(File.ReadAllText(arguments[i + 1]));
                        if (runtimeConfiguration != null)
                        {
                            Output.Log("Runtime configuration loaded");
                            Program.runtimeConfiguration = runtimeConfiguration;
                        }
                        else
                        {
                            Output.Error("Runtime configuration failed to load");
                            Environment.Exit(1);
                        }
                        break;

                }
            }
        }

    }
}


// Output.Error($"Invalid argument '{arg}'\nRun the application with '-h' or '--help' for valid arguments.");