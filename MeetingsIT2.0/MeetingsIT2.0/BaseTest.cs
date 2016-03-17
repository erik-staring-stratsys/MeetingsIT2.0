using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

        protected virtual void PostStart()
        {
        }

        protected virtual void SetUp()
        {
        }

        protected virtual void PreTearDown()
        {
        }

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
        public abstract class ApiMeetingV2BaseTest<T> : BaseTest<T> where T : BasePage
        {
            protected override void PostStart()
            {
                _driver.Manage().Window.Maximize();
                _driver.WaitFor(_page.Explore);

                _driver.Click(_page.ApiSelectorOptions.Skip(1).First());

                _driver.Click(_page.Explore, _page.MeetingV2EndpointList);

                _driver.Click(_page.MeetingV2EndpointList, _page.PostMeetings);

                _driver.Click(_page.ExpandPostMeetings(_page.PostMeetings), _page.Authenticate(_page.PostMeetings));

                _driver.Click(_page.Authenticate(_page.PostMeetings), _page.AuthenticateDialog);

                _driver.Click(_page.MeetingsScope);

                _driver.Click(_page.AuthorizeButton, null, "1000");

                _driver.SwitchTo().Window(_driver.WindowHandles.Last());
                _driver.WaitFor(_page.UserName);

                _page.UserName.SendKeys("stratsysseleniumtests@gmail.com");
                _page.Password.SendKeys("testa");
                _driver.Click(_page.LoginButton, null, "1000");

                _driver.SwitchTo().Window(_driver.WindowHandles.First());
            }
        }

        [TestFixture]
        public abstract class ApiUserV2BaseTest<T> : BaseTest<T> where T : BasePage
        {
            protected override void PostStart()
            {
                _driver.Manage().Window.Maximize();
                _driver.WaitFor(_page.Explore);

                _driver.Click(_page.ApiSelectorOptions.Skip(1).First());

                _driver.Click(_page.Explore, _page.MeetingV2EndpointList);

                _driver.Click(_page.MeetingV2EndpointList, _page.PostMeetings);
            }
        }
    }
}

