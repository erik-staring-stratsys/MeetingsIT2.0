using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using MeetingsIT2;
using MeetingsTests.Dto;
using NUnit.Framework;

namespace MeetingsTests.Api
{
    public class MeetingV2Tests : BaseTest<MeetingV2Page>.ApiMeetingV2BaseTest<MeetingV2Page>
    {
        protected override MeetingV2Page CreatePage()
        {
            return new MeetingV2Page(_driver);
        }

        [Test]
        public void GetMeeting()
        {
            var meetingDto = _page.PostMeeting();

            _driver.Click(_page.ExpandSection(_page.GetMeeting), _page.Submit(_page.GetMeeting));
            
            _page.IdParameterInput(_page.GetMeeting).SendKeys(meetingDto.Id);
            _driver.Click(_page.Submit(_page.GetMeeting), null ,"1000");

            var meetingId = _page.ResponseId(_page.ResponseBodys.Last()).Text;
            var meetingDate = _page.ResponseDate(_page.ResponseBodys.Last()).Text.Replace("\"","");
            var meetingName = _page.ResponseMeetingName(_page.ResponseBodys.Last()).Text.Replace("\"","");
            var meetingStartTime = _page.ResponseMeetingStartTime(_page.ResponseBodys.Last()).Text.Replace("\"", ""); 
            var meetingEndTime = _page.ResponseMeetingEndTime(_page.ResponseBodys.Last()).Text.Replace("\"", "");
            var meetingDescription = _page.ResponseMeetingDescription(_page.ResponseBodys.Last()).Text.Replace("\"", "");
            var meetingLocation = _page.ResponseMeetingLocation(_page.ResponseBodys.Last()).Text.Replace("\"", "");

            Assert.That(meetingId, Is.EqualTo(meetingDto.Id));
            Assert.That(meetingDate, Is.EqualTo(meetingDto.Date));
            Assert.That(meetingName, Is.EqualTo(meetingDto.MeetingName));
            Assert.That(meetingStartTime, Is.EqualTo(meetingDto.StartTime));
            Assert.That(meetingEndTime, Is.EqualTo(meetingDto.EndTime));
            Assert.That(meetingDescription, Is.EqualTo(meetingDto.Description));
            Assert.That(meetingLocation, Is.EqualTo(meetingDto.Location));
        }

        [Test]
        public void DeleteMeeting()
        {
            var meetingDto = _page.PostMeeting();

            _driver.Click(_page.ExpandSection(_page.DeleteMeeting), _page.Submit(_page.DeleteMeeting));

            _page.IdParameterInput(_page.DeleteMeeting).SendKeys(meetingDto.Id);
            _driver.Click(_page.Submit(_page.DeleteMeeting), null, "1000");
            Assert.That(_page.ResponseCode(_page.Endpoints.Skip(2).First()).Text, Is.EqualTo("200"));

            _driver.Click(_page.ExpandSection(_page.GetMeeting), _page.Submit(_page.GetMeeting));

            _page.IdParameterInput(_page.GetMeeting).SendKeys(meetingDto.Id);
            _driver.Click(_page.Submit(_page.GetMeeting), null, "1000");
            Assert.That(_page.ResponseCode(_page.Endpoints.Skip(3).First()).Text, Is.EqualTo("500"));
        }

        [Test]
        public void PutMeeting()
        {
            var meetingDto = _page.PostMeeting();

            _driver.Click(_page.ExpandSection(_page.PutMeetingSection), _page.Submit(_page.PutMeetingSection));

            var id = meetingDto.Id;
            var updatedMeetingDto = _page.PutMeeting(id);
            Assert.That(meetingDto.Id, Is.EqualTo(updatedMeetingDto.Id));

            _driver.Click(_page.ExpandSection(_page.GetMeeting), _page.Submit(_page.GetMeeting));

            _page.IdParameterInput(_page.GetMeeting).SendKeys(meetingDto.Id);
            _driver.Click(_page.Submit(_page.GetMeeting), null, "1000");

            var meetingId = _page.ResponseId(_page.ResponseBodys.Last()).Text;
            var meetingDate = _page.ResponseDate(_page.ResponseBodys.Last()).Text.Replace("\"", "");
            var meetingName = _page.ResponseMeetingName(_page.ResponseBodys.Last()).Text.Replace("\"", "");
            var meetingStartTime = _page.ResponseMeetingStartTime(_page.ResponseBodys.Last()).Text.Replace("\"", "");
            var meetingEndTime = _page.ResponseMeetingEndTime(_page.ResponseBodys.Last()).Text.Replace("\"", "");
            var meetingDescription = _page.ResponseMeetingDescription(_page.ResponseBodys.Last()).Text.Replace("\"", "");
            var meetingLocation = _page.ResponseMeetingLocation(_page.ResponseBodys.Last()).Text.Replace("\"", "");

            Assert.That(meetingId, Is.EqualTo(meetingDto.Id));
            Assert.That(meetingDate, Is.EqualTo(meetingDto.Date));
            Assert.That(meetingName, Is.EqualTo(meetingDto.MeetingName));
            Assert.That(meetingStartTime, Is.EqualTo(meetingDto.StartTime));
            Assert.That(meetingEndTime, Is.EqualTo(meetingDto.EndTime));
            Assert.That(meetingDescription, Is.EqualTo(meetingDto.Description));
            Assert.That(meetingLocation, Is.EqualTo(meetingDto.Location));
        }
    }
}
 

    