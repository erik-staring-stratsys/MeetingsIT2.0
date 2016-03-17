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
    public class MeetingV2Page : BasePage
    {
        public MeetingV2Page(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public MeetingDto PostMeeting()
        {
            var date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            var meetingName = GenerateName("Meeting");
            var description = GenerateName("Description");
            var startTime = "12:00:00";
            var endTime = "13:00:00";
            var location = GenerateName("Location");
            
            DtoTextarea.SendKeys($"{{'MeetingName': '{meetingName}','Date': '{date}', 'Description': '{description}', 'StartTime': '{startTime}', 'EndTime': '{endTime}', 'Location': '{location}'}}");

            Submit(PostMeetings).Click();
            _driver.WaitFor(ResponseBody);

            var meetingDto = new MeetingDto();
            var meetingId = ResponseId(ResponseBody).Text;

            meetingDto.Id = meetingId;
            meetingDto.Date = date;
            meetingDto.MeetingName = meetingName;
            meetingDto.Description = description;
            meetingDto.StartTime = startTime;
            meetingDto.EndTime = endTime;
            meetingDto.Location = location;

            return meetingDto;
        }

        public MeetingDto PutMeeting(string meetingId)
        {
            IdParameterInput(PutMeetingSection).SendKeys(meetingId);

            var date = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var meetingName = GenerateName("MeetingUpdated");
            var description = GenerateName("DescriptionUpdated");
            var startTime = "15:00:00";
            var endTime = "16:00:00";
            var location = GenerateName("LocationUpdated");

            DtoTextarea.SendKeys($"{{'MeetingName': '{meetingName}','Date': '{date}', 'Description': '{description}', 'StartTime': '{startTime}', 'EndTime': '{endTime}', 'Location': '{location}'}}");

            Submit(PutMeetingSection).Click();
            _driver.WaitFor(ResponseBody);

            var updatedMeetingDto = new MeetingDto();
            var updatedMeetingId = ResponseId(ResponseBodys.Last()).Text;

            updatedMeetingDto.Id = updatedMeetingId;
            updatedMeetingDto.Date = date;
            updatedMeetingDto.MeetingName = meetingName;
            updatedMeetingDto.Description = description;
            updatedMeetingDto.StartTime = startTime;
            updatedMeetingDto.EndTime = endTime;
            updatedMeetingDto.Location = location;

            return updatedMeetingDto;
        }

        [FindsBy(How = How.Id, Using = "MeetingV2_MeetingV2_GetMeeting")]
        public IWebElement GetMeeting { get; set; }

        [FindsBy(How = How.Id, Using = "MeetingV2_MeetingV2_DeleteMeeting")]
        public IWebElement DeleteMeeting { get; set; }

        [FindsBy(How = How.Id, Using = "MeetingV2_MeetingV2_PutMeeting")]
        public IWebElement PutMeetingSection { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(text(), 'Click to set as parameter value')]")]
        public IWebElement ModelSchema { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".body-textarea.required")]
        public IWebElement DtoTextarea { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".response_body.json")]
        public IWebElement ResponseBody { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".response_body.json")]
        public IList<IWebElement> ResponseBodys { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".endpoint")]
        public IList<IWebElement> Endpoints { get; set; }

        public IWebElement ResponseId(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).First();
            return element;
        }

        public IWebElement ResponseDate(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(2).First();
            return element;
        }

        public IWebElement ResponseCode(IWebElement endpoint)
        {
            var element = endpoint.FindElement(By.CssSelector(".block.response_code"));
            return element;
        }

        public IWebElement ResponseMeetingName(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(1).First();
            return element;
        }

        public IWebElement ResponseMeetingStartTime(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(4).First();
            return element;
        }

        public IWebElement ResponseMeetingEndTime(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(5).First();
            return element;
        }

        public IWebElement ResponseMeetingDescription(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(3).First();
            return element;
        }

        public IWebElement ResponseMeetingLocation(IWebElement responseBody)
        {
            var element = responseBody.FindElements(By.CssSelector(".value")).Skip(6).First();
            return element;
        }

        public IWebElement ExpandSection(IWebElement section)
        {
            var element = section.FindElement(By.CssSelector(".toggleOperation"));
            return element;
        }

        public IWebElement IdParameterInput(IWebElement section)
        {
            var element = section.FindElement(By.CssSelector(".parameter.required"));
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