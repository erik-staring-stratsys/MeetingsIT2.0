using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace MeetingsIT2
{
    public static class WebDriverExtensions
    {
        public static IWebElement WaitFor(this IWebDriver driver, IWebElement elementFromPage)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            var element = wait.Until(ElementIsClickable(elementFromPage));
            Thread.Sleep(1000);
            return element;
        }

        public static void Hover(this IWebDriver driver, IWebElement element)
        {
            Thread.Sleep(500);
            Actions hoverEvent = new Actions(driver);
            hoverEvent.MoveToElement(element).Build().Perform();
        }

        public static void HoverAndClick(this IWebDriver driver, IWebElement element, By elementToClick)
        {
            Thread.Sleep(500);
            Actions hoverEvent = new Actions(driver);
            hoverEvent.MoveToElement(element).Perform();
            Thread.Sleep(2000);
            hoverEvent.MoveToElement(element.FindElement(elementToClick)).Click().Perform();
        }
        
        private static Func<IWebDriver, IWebElement> ElementIsClickable(IWebElement element)
        {
            return driver =>
            {
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            };
        }

        public static void WaitForOneElementLessOrMore(this IWebDriver driver, int numberOfElementsAfter, int numberOfElementsBefore, string @class)
        {
            var count = 0;
            while (numberOfElementsAfter == numberOfElementsBefore)
            {
                Thread.Sleep(200);
                count++;
                numberOfElementsAfter = driver.FindElementsByCssSelector(@class).Count;
                if (count > 20)
                {
                    break;
                }
            }
            Thread.Sleep(200);
        }

        public static IWebElement FindElementByCssSelector(this IWebDriver driver, string selector)
        {
            return driver.FindElement(By.CssSelector(selector));
        }
        
        public static ICollection<IWebElement> FindElementsByCssSelector(this IWebDriver driver, string selector)
        {
            return driver.FindElements(By.CssSelector(selector));
        }

        public static void SendKeys(this IWebDriver driver, string keys)
        {
            var webDriver = driver as IHasInputDevices;

            if (webDriver == null)
            {
                throw new Exception("Driver doues not support input devices");
            }
            webDriver.Keyboard.SendKeys(keys);
        }

        public static void ScrollToElement(this IWebDriver driver, IWebElement element)
        {
            driver.ExecuteJavaScript<IWebElement>("arguments[0].scrollIntoView(true);", element);
            Thread.Sleep(500);
        }

        public static bool IsElementPresent(this IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static void JsErrorCheck(this IWebDriver driver)
        {
            Thread.Sleep(500);
            var js = driver as IJavaScriptExecutor;
            ICollection javascriptErrors = null;
            for (var i = 0; i < 20; i++)
            {
                javascriptErrors = js.ExecuteScript("return window.jsErrors") as ICollection;
                if (javascriptErrors != null) break;
                Thread.Sleep(1000);
            }
            Assert.IsNotNull(javascriptErrors, "Can't seem to load JavaScript on the page to find JavaScript errors. Check that JavaScript is enabled.");
            var javaScriptErrorsAsString = javascriptErrors.Cast<string>().Aggregate("", (current, error) => current + (error + ", "));
            Assert.AreEqual("", javaScriptErrorsAsString, "Found JavaScript errors on page: " + javaScriptErrorsAsString);
        }

        public static void Click(this IWebDriver driver, IWebElement element, IWebElement elementToWaitFor = null)
        {
            element.Click();
            driver.JsErrorCheck();
            if (elementToWaitFor != null)
            {
                driver.WaitFor(elementToWaitFor);
                driver.JsErrorCheck();
            }
        }
    }
}