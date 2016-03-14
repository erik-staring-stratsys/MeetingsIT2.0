using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MeetingsIT2
{
    public static class WebElementExtensions
    {
        public static string Value(this IWebElement webElement)
        {
            return webElement.GetAttribute("value");
        }

        public static string Title(this IWebElement webElement)
        {
            return webElement.GetAttribute("title");
        }

        public static void WaitForElementsNotDisplayed(this IWebElement element)
        {
            var count = 0;
            while (element.Displayed)
            {
                Thread.Sleep(200);
                count++;
                if (count > 50)
                {
                    break;
                }
            }
            Thread.Sleep(200);
        }

        public static void WaitForElementsDisplayed(this IWebElement element)
        {
            var count = 0;
            while (!element.Displayed)
            {
                Thread.Sleep(200);
                count++;
                if (count > 10)
                {
                    break;
                }
            }
            Thread.Sleep(200);
        }

    }
}
