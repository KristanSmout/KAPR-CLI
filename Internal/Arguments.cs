using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPR_CLI.Internal
{
    internal class Arguments
    {
        public static List<ArgumentDetails> argumentList = new List<ArgumentDetails>
        {
            new ArgumentDetails { Arguments = "-h, --help", Description = "Displays this help message" },

        };

        public class ArgumentDetails
        {
            public string Arguments { get; set; }
            public string Description { get; set; }
        }

        public static void parseArguments(string[] arguments)
        {
            foreach (var arg in arguments)
            {
                switch (arg)
                {
                    case "-h":
                    case "--help":
                        printArgumentsTable();
                        break;

                    case "-v":
                    case "--version":
                        Output.Log("KAPR CLI v0.1.0");
                        break;

                    case "-a":
                    case "--actions":
                        Actions.PrintActionTable();
                        break;
                    default:
                        Output.Error($"Invalid argument '{arg}'\nRun the application with '-h' or '--help' for valid arguments.");
                        break;
                }
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

    }
}
