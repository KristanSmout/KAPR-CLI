using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KAPR_CLI.Internal.Arguments;

namespace KAPR_CLI.Internal
{
    internal class Actions
    {
        public static List<ActionDetails> actionList = new List<ActionDetails>
        {
            new ActionDetails { Action = "-h, --help", Description = "Displays this help message", Example = "" },

        };

        public class ActionDetails
        {
            public required string Action { get; set; }
            public string Description { get; set; } = "Usnet";
            public string Example { get; set; } = "Usnet";
        }

        public static void PrintActionTable()
        {
            int maxActionLength = Math.Max("Action".Length, actionList.Max(a => a.Action.Length));
            int maxDescriptionLength = Math.Max("Description".Length, actionList.Max(a => a.Description.Length));
            int maxExampleLength = Math.Max("Example".Length, actionList.Max(a => a.Example.Length));

            Console.WriteLine($"| {"Action".PadRight(maxActionLength)} | {"Description".PadRight(maxDescriptionLength)} | {"Example".PadRight(maxExampleLength)} |");

            Console.WriteLine($"|{new string('-', maxActionLength + 2)}|{new string('-', maxDescriptionLength + 2)}|{new string('-', maxExampleLength + 2)}|");

            foreach (var action in actionList)
            {
                Console.WriteLine($"| {action.Action.PadRight(maxActionLength)} | {action.Description.PadRight(maxDescriptionLength)} | {action.Example.PadRight(maxExampleLength)} |");
            }
        }



        //Actions



    }
}
