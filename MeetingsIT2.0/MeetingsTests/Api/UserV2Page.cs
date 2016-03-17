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