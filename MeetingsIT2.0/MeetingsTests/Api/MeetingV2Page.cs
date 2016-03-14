using MeetingsIT2;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace MeetingsTests.Api
{
    public class MeetingV2Page : BasePage
    {
        public MeetingV2Page(IWebDriver driver) : base(driver)
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