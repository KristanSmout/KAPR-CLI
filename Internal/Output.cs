using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPR_CLI.Internal
{
    internal class Output
    {

        public static List<string> executionLog = new List<string>();

        public static void Log(string message)
        {
            DateTime date = DateTime.Now;
            string time = date.ToString("dd/MM/yyyy h:mm:ss tt");
            Console.WriteLine($" {time} | Log | {message}");
            executionLog.Add($" {time} | Log | {message}");
        }

        public static void Warning(string message)
        {
            DateTime date = DateTime.Now;
            string time = date.ToString("dd/MM/yyyy h:mm:ss tt");
            Console.WriteLine($" {time} | Warning | {message}");
            executionLog.Add($" {time} | Warning | {message}");
        }

        public static void Error(string message)
        {
            DateTime date = DateTime.Now;
            string time = date.ToString("dd/MM/yyyy h:mm:ss tt");
            Console.WriteLine($" {time} | Error | {message}");
            executionLog.Add($" {time} | Error | {message}");

            //Send Email
            if (Program.runtimeConfiguration.sendEmail == true)
            {
                Output.Debug("Sending Email");
                try
                {
                    Utilities.sendEmail(message);
                }
                catch (Exception e)
                {
                    Output.Critical("Failed to send email");
                    Output.Critical(e.Message);
                }
            }
        }

        public static void Critical(string message)
        {
            DateTime date = DateTime.Now;
            string time = date.ToString("dd/MM/yyyy h:mm:ss tt");
            Console.WriteLine($" {time} | Critical | {message}");
            executionLog.Add($" {time} | Critical | {message}");
        }

        public static void Debug(string message)
        {
            if (KAPR_CLI.Program.debugMode == false)
            {
                return;
            }
            else
            {
                DateTime date = DateTime.Now;
                string time = date.ToString("dd/MM/yyyy h:mm:ss tt");
                Console.WriteLine($" {time} | Debug | {message}");
                executionLog.Add($" {time} | Debug | {message}");
            }
        }

    }
}
