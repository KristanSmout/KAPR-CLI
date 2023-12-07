using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static KAPR_CLI.Internal.Arguments;

namespace KAPR_CLI.Internal
{
    internal class Actions
    {

        public static int count = 0;
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

        //Enums
        public enum Existance
        {
            Exists,
            DoesNotExist,
            Visible,
            Hidden
        }

        public enum Equals
        {
            Matches,
            DoesNotMatch,
            Return
        }

        public enum LocatorType
        {
            Id,
            CssSelector,
            XPath,
            Text,
            LinkText,
            PartialLinkText,
            Name,
            TagName,
            JavaScript
        }

        public enum JSQuery
        {
            Run,
            Return
        }




        //Actions
        public static void ExecuteCommand(string command)
        {
            count++;
            string[] parts = command.Split(' ');
            string action = parts[0].ToLower();
            try
            {
                switch (action)
                {
                    case "click":
                        if (parts.Length >= 3)
                        {
                            LocatorType type = (LocatorType)Enum.Parse(typeof(LocatorType), parts[1], true);

                            if (parts.Length > 3)
                            {
                                for (int i = 3; i < parts.Length; i++)
                                {
                                    parts[2] += $" {parts[i]}";
                                }
                            }

                            string value = parts[2];
                            Click(type, value);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for Click: {parts.Length}");
                        }
                        break;
                    case "navigate":
                        if (parts.Length == 2)
                        {
                            string url = parts[1];
                            try
                            {
                                Navigate(url);
                            }
                            catch (Exception e)
                            {
                                Output.Error($"Error Navigating to {url}: {e.Message}");
                            }
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for Navigate: {parts.Length}");
                        }
                        break;
                    case "screenshot":
                        if (parts.Length == 2)
                        {
                            string name = parts[1];
                            TakeScreenshot(name);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for Screenshot: {parts.Length}");
                        }
                        break;
                    case "snapshot":
                        if (parts.Length == 2)
                        {
                            string htmlcontent = Program.driver.PageSource;
                            htmlcontent = Internal.Utilities.FixLocalReferencesInHtml(ref htmlcontent, Program.baseURL);
                            TakeSnapshot(htmlcontent, parts[1]);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for Snapshot: {parts.Length}");
                        }
                        break;
                    case "setvalue":
                        if (parts.Length >= 4)
                        {
                            LocatorType type = (LocatorType)Enum.Parse(typeof(LocatorType), parts[1], true);
                            string value = parts[2];

                            if (parts.Length > 4)
                            {
                                for (int i = 4; i < parts.Length; i++)
                                {
                                    parts[3] += $" {parts[i]}";
                                }
                            }

                            string text = parts[3];
                            SetValue(type, value, text);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for SetValue: {parts.Length}");
                        }
                        break;
                    case "readvalue":
                        if (parts.Length >= 3)
                        {
                            LocatorType type = (LocatorType)Enum.Parse(typeof(LocatorType), parts[1], true);

                            if (parts.Length > 3)
                            {
                                for (int i = 3; i < parts.Length; i++)
                                {
                                    parts[2] += $" {parts[i]}";
                                }
                            }

                            string value = parts[2];
                            ReadValue(type, value);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for ReadValue: {parts.Length}");
                        }
                        break;
                    case "sleep":
                        if (parts.Length == 2)
                        {
                            int ms = int.Parse(parts[1]);
                            Sleep(ms);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for Sleep: {parts.Length}");
                        }
                        break;
                    case "waitfor":
                        if (parts.Length >= 4)
                        {
                            LocatorType type = (LocatorType)Enum.Parse(typeof(LocatorType), parts[1], true);
                            Existance existance = (Existance)Enum.Parse(typeof(Existance), parts[2], true);

                            if (parts.Length > 4)
                            {
                                for (int i = 4; i < parts.Length; i++)
                                {
                                    parts[3] += $" {parts[i]}";
                                }
                            }

                            string value = parts[3];
                            WaitFor(type, value, existance);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for WaitFor: {parts.Length}");
                        }
                        break;
                    case "write":
                        if (parts.Length >= 2)
                        {
                            if (parts.Length > 2)
                            {
                                for (int i = 2; i < parts.Length; i++)
                                {
                                    parts[1] += $" {parts[i]}";
                                }
                            }

                            Output.Debug(parts[1]);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for Write: {parts.Length}");
                        }
                        break;
                    case "url":
                        if (parts.Length == 3)
                        {
                            Equals match = (Equals)Enum.Parse(typeof(Equals), parts[1], true);
                            string url = parts[2];
                            URLInfo(match, url);

                        }
                        else if (parts.Length == 2)
                        {
                            Equals match = (Equals)Enum.Parse(typeof(Equals), parts[1], true);
                            URLInfo(match);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for URL: {parts.Length}");
                        }
                        break;
                    case "javascript":
                        if (parts.Length >= 3)
                        {
                            if (parts.Length > 3)
                            {
                                for (int i = 3; i < parts.Length; i++)
                                {
                                    parts[2] += $" {parts[i]}";
                                }
                            }

                            JSQuery feedback = (JSQuery)Enum.Parse(typeof(JSQuery), parts[1], true);
                            RunJavascript(feedback, parts[2]);

                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for Javascript: {parts.Length}");
                        }
                        break;
                    case "waitforload":
                        WaitForPageToComplete();
                        break;
                    case "routine":
                        if (parts.Length == 2)
                        {
                            RunFunction(parts[1]);
                        }
                        else
                        {
                            Output.Debug($"Invalid number of arguments for Function: {parts.Length}");
                        }
                        break;
                }

                if (Program.runtimeConfiguration.forceScreenshot) ;
                {
                    TakeScreenshot($"{count.ToString()}_Forced");
                }
            }
            catch (Exception e)
            {
                Output.Error($"Error Executing Command: {command} \n {e.Message}");

            }

        }

        public static void Click(LocatorType type, string value)
        {
            IWebElement element = (Utilities.element(type, value));

            if (element != null)
            {
                element.Click();
            }
            else
            {
                Output.Error($"Element {value} Not Found");
            }
        }

        public static void Navigate(string url)
        {
            Program.driver.Navigate().GoToUrl(url);
            Program.baseURL = url;
        }

        public static void TakeSnapshot(string html, string name)
        {
            File.WriteAllText($"{Program.outputDirectory}\\snapshots\\{name}.html", html);
        }

        public static void TakeScreenshot(string name)
        {
            var screenshot = Program.driver.GetScreenshot();
            try
            {
                screenshot.SaveAsFile($"{Program.outputDirectory}\\screenshots\\{name}.jpeg", ScreenshotImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Output.Error($"Error Saving Screenshot: {e.Message}");
            }

        }

        public static void SetValue(LocatorType type, string value, string text)
        {
            IWebElement element = (Utilities.element(type, value));

            if (element != null)
            {
                element.SendKeys(text);
            }
        }

        public static void ReadValue(LocatorType type, string value)
        {
            IWebElement element = (Utilities.element(type, value));
            if (element != null)
            {
                element.GetAttribute("value");
            }
        }

        public static void Sleep(int ms)
        {
            Output.Debug($"Sleep Starting for {ms.ToString()}ms at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
            Thread.Sleep(ms);
            Output.Debug($"Sleep Ending at {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
        }

        public static void WaitFor(LocatorType type, string value, Existance existance)
        {
            IWebElement element = (Utilities.element(type, value));
            bool complete = false;
            int count = 0;
            while (!complete)
            {
                if (count < Program.runtimeConfiguration.timeout * 10)
                {
                    element = (Utilities.element(type, value));
                    if (existance == Existance.Exists)
                    {
                        if (element != null)
                        {
                            Output.Debug($"Element {value} Found After {count.ToString()} Attempts");
                            complete = true;
                        }
                    }
                    else if (existance == Existance.DoesNotExist)
                    {
                        if (element == null)
                        {
                            Output.Debug($"Element {value} Vanished After {count.ToString()} Attempts");
                            complete = true;
                        }
                    }
                    else if (existance == Existance.Visible)
                    {
                        if (element.Displayed)
                        {
                            Output.Debug($"Element {value} Visible After {count.ToString()} Attempts");
                            complete = true;
                        }
                    }
                    else if (existance == Existance.Hidden)
                    {
                        if (element == null)
                        {
                            Output.Debug($"Element {value} Hidden (Removed) After {count.ToString()} Attempts");
                            complete = true;
                        }
                        else if (!element.Displayed)
                        {
                            Output.Debug($"Element {value} Not Visible After {count.ToString()} Attempts");
                            complete = true;
                        }
                    }

                    Thread.Sleep(100);
                    count++;
                }
                else
                {
                    Output.Error($"Element {value} Not Found After {Program.runtimeConfiguration.timeout.ToString()} Seconds");
                }
            }
        }

        public static void URLInfo(Equals action, string value = null)
        {
            string url = Program.driver.Url;
            switch (action)
            {

                case Equals.Matches:
                    if (url == value)
                    {
                        Output.Debug($"URL Matches {value}");
                    }
                    else
                    {
                        Output.Error($"URL Does Not Match {value}");
                    }
                    break;

                case Equals.DoesNotMatch:
                    if (url != value)
                    {
                        Output.Debug($"URL Does Not Match {value}");
                    }
                    else
                    {
                        Output.Error($"URL Matches {value}");
                    }
                    break;

                case Equals.Return:
                    Output.Debug(url);
                    break;
                default:
                    Output.Debug($"Invalid Equals Action: {action}");
                    break;
            }
        }

        public static void RunJavascript(JSQuery action, string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Program.driver;
            if (action == JSQuery.Run)
            {
                js.ExecuteScript(script);
            }
            else
            {
                object result = js.ExecuteScript(script);
                string resultAsString = result != null ? result.ToString() : "null";
                Output.Debug("JS Response: " + resultAsString);
            }


        }

        public static void WaitForPageToComplete()
        {
            Output.Debug("Waiting for page to finish loading");
            try
            {
                WebDriverWait wait = new WebDriverWait(Program.driver, TimeSpan.FromSeconds((double)Program.runtimeConfiguration.timeout));
                wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
                Output.Debug("Page Loaded");
            }
            catch (TimeoutException e)
            {
                Output.Error("Page took too long to load");
            }

        }

        public static void RunFunction(string name)
        {
            if (Program.runtimeConfiguration.functionFileDirectory != null)
            {
                string[] lines = File.ReadAllLines($"{Program.runtimeConfiguration.functionFileDirectory}\\{name}.txt");
                Output.Debug($"Executing Function: {name}");
                foreach (string line in lines)
                {
                    ExecuteCommand(line);
                }
                Output.Debug($"Function {name} Complete");
            }
            else
            {
                Output.Error("No Function Directory Configured");
            }
        }



    }
}
