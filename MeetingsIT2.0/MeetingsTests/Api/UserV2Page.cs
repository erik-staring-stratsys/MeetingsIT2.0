using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using MeetingsIT2;
using MeetingsTests.Dto;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MeetingsTests.Api
{
    public class UserV2Page : BasePage
    {
        public UserV2Page(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".response_body.json")]
        public IWebElement ResponseBody { get; set; }

        public IWebElement ResponseName(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).First();
            return element;
        }

        public IWebElement ResponseEmail(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(1).First();
            return element;
        }

        public IWebElement ResponseId(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(2).First();
            return element;
        }

        public IWebElement ResponseIsExternal(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(3).First();
            return element;
        }

        public IWebElement ResponseInitials(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(4).First();
            return element;
        }

        public IWebElement ResponseGravatarId(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(5).First();
            return element;
        }

        public IWebElement ResponseLabel(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(6).First();
            return element;
        }

        public IWebElement ResponseHasFinishedMeetingGuide(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(7).First();
            return element;
        }

        public IWebElement ResponseHasAcceptedTerms(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(8).First();
            return element;
        }

        public IWebElement ResponseHasFinishedIntroGuide(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(9).First();
            return element;
        }

        public IWebElement ResponseCanAddUsers(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(10).First();
            return element;
        }

        public IWebElement ResponseIsOrganizerForAnyMeeting(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(11).First();
            return element;
        }

        public IWebElement ResponseIsAdmin(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value .literal")).Last();
            return element;
        }
        
#if TEST
        public override string Url
        {
            get { return "https://test.runyourmeeting.com/swagger/"; }
        }
#else
        public override string Url
        {
            get { return "https://test.runyourmeeting.com/swagger/"; }
        }
#endif
    }
}