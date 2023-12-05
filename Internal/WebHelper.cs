using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KAPR_CLI.Internal
{
    internal class WebHelper
    {
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

        public static IWebElement getElementByLocator(WebHelper.LocatorType type, string value)
        {
            IWebElement element = null;

            try
            {

                switch (type)
                {
                    case LocatorType.Id:
                        element = Program.driver.FindElement(By.Id(value));
                        break;

                    case LocatorType.CssSelector:
                        element = Program.driver.FindElement(By.CssSelector(value));
                        break;

                    case LocatorType.XPath:
                        element = Program.driver.FindElement(By.XPath(value));
                        break;

                    case LocatorType.Text:
                        element = Program.driver.FindElement(By.XPath($"//*[text()='{value}']"));
                        break;

                    case LocatorType.LinkText:
                        element = Program.driver.FindElement(By.LinkText(value));
                        break;

                    case LocatorType.PartialLinkText:
                        element = Program.driver.FindElement(By.PartialLinkText(value));
                        break;

                    case LocatorType.Name:
                        element = Program.driver.FindElement(By.Name(value));
                        break;

                    case LocatorType.TagName:
                        element = Program.driver.FindElement(By.TagName(value));
                        break;

                    case LocatorType.JavaScript:
                        IJavaScriptExecutor js = (IJavaScriptExecutor)Program.driver;
                        element = (IWebElement)js.ExecuteScript($"return document.querySelector('{value}')");
                        break;

                    default:
                        Output.Log($"Locator Type {type} not found: {value}");
                        break;
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
