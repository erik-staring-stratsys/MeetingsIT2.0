using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MeetingsIT2
{
    public abstract class BasePage
    {
        protected readonly IWebDriver _driver;

        protected BasePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GenerateName(string prefix)
        {
            return $"{prefix}.{DateTime.Now.ToString("yyyyMMdd.HHmmssff")}";
        }

        [FindsBy(How = How.Id, Using = "explore")]
        public IWebElement Explore { get; set; }

        [FindsBy(How = How.Id, Using = "scope_0")]
        public IWebElement MeetingsScope { get; set; }

        [FindsBy(How = How.Id, Using = "txtUserName")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a[data-id='MeetingV2']")]
        public IWebElement MeetingV2EndpointList { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".loginButton")]
        public IWebElement LoginButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".api-popup-dialog")]
        public IWebElement AuthenticateDialog { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".api-popup-authbtn")]
        public IWebElement AuthorizeButton { get; set; }

        [FindsBy(How = How.Id, Using = "MeetingV2_MeetingV2_PostMeeting")]
        public IWebElement PostMeetings { get; set; }
        
        public IWebElement ExpandPostMeetings(IWebElement postMeetings)
        {
            var element = postMeetings.FindElement(By.CssSelector(".toggleOperation"));
            return element;
        }
        
        public IWebElement Authenticate(IWebElement postMeetings)
        {
            var element = postMeetings.FindElement(By.CssSelector(".api-ic.ic-off"));
            return element;
        }

        public IWebElement Submit(IWebElement postMeetings)
        {
            var element = postMeetings.FindElement(By.CssSelector(".submit"));
            return element;
        }
        
        [FindsBy(How = How.CssSelector, Using = "option")]
        public IList<IWebElement> ApiSelectorOptions { get; set; }

        public void GoToStart()
        {
            _driver.Navigate().GoToUrl(Url);
        }

        public abstract string Url { get; }
    }
}
