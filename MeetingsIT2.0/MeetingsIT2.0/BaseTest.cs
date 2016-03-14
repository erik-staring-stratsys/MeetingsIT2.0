using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MeetingsIT2
{
    [TestFixture]
    public abstract class BaseTest<T> where T : BasePage
    {
        protected IWebDriver _driver;
        protected T _page;

        protected abstract T CreatePage();

        [SetUp]
        public void SetUpBase()
        {
            StartDriver();
            _page = CreatePage();
            _page.GoToStart();
            PostStart();
            SetUp();
        }

        protected virtual void PostStart() { }

        protected virtual void SetUp() { }
        
        protected virtual void PreTearDown() { }
        
        [TearDown]
        public void TearDown()
        {
            _driver.Manage().Window.Maximize();
            PreTearDown();
            _driver.Quit();
        }

        private void StartDriver()
        {
#if DEBUG && !BUILDAGENT            
            _driver = new ChromeDriver(@"C:\GIT\MeetingsIT2.0\drivers", new ChromeOptions
            {
                LeaveBrowserRunning = false
            });
#else
            var remoteAddress = new Uri("http://buildagent3:4444/wd/hub");
            var desiredCapabilities = DesiredCapabilities.Chrome();
            _driver = new RemoteWebDriver(remoteAddress, desiredCapabilities);
#endif
        }

        [TestFixture]
        public abstract class ApiBaseTest<T> : BaseTest<T> where T : BasePage
        {
            protected override void PostStart()
            {
                _driver.Manage().Window.Maximize();
                _driver.WaitFor(_page.Explore);

                _page.ApiSelectorOptions.Skip(1).First().Click();

                _page.Explore.Click();
                _driver.WaitFor(_page.MeetingV2EndpointList);

                _page.MeetingV2EndpointList.Click();
                _driver.WaitFor(_page.PostMeetings);

                _page.ExpandPostMeetings(_page.PostMeetings).Click();
                _driver.WaitFor(_page.Authenticate(_page.PostMeetings));

                _page.Authenticate(_page.PostMeetings).Click();
                _driver.WaitFor(_page.AuthenticateDialog);

                _page.MeetingsScope.Click();

                _page.AuthorizeButton.Click();
                Thread.Sleep(1000);
                _driver.SwitchTo().Window(_driver.WindowHandles.Last());
                _driver.WaitFor(_page.UserName);

                _page.UserName.SendKeys("stratsysseleniumtests@gmail.com");
                _page.Password.SendKeys("testa");
                _page.LoginButton.Click();
                Thread.Sleep(1000);

                _driver.SwitchTo().Window(_driver.WindowHandles.First());

                _page.Submit(_page.PostMeetings).Click();

            }
        }
    }
}

