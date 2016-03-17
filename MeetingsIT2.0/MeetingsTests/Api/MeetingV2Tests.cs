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
    [Parallelizable(ParallelScope.Fixtures)]
    public class MeetingV2Tests : BaseTest<MeetingV2Page>.ApiMeetingV2BaseTest<MeetingV2Page>
    {
        protected override MeetingV2Page CreatePage()
        {
            return new MeetingV2Page(_driver);
        }

        [Test]
        public void GetMeetings()
        {
            var meetingDto = _page.PostMeeting();

            _driver.Click(_page.ExpandSection(_page.GetMeetings), _page.Submit(_page.GetMeetings));

            _driver.Click(_page.Submit(_page.GetMeetings), null, "1000");

            var firstMeetingId = _page.ResponseId(_page.GetMeetings).Text;
            var firstMeetingName = _page.ResponseMeetingName(_page.GetMeetings).TextOnly();
            Assert.That(firstMeetingId, Is.EqualTo("2214"));
            Assert.That(firstMeetingName, Is.EqualTo("test"));

            var lastMeetingId = _page.ResponseLastId(_page.GetMeetings, meetingDto.Id).Text;
            Assert.That(lastMeetingId, Is.EqualTo(meetingDto.Id));
        }

        [Test]
        public void GetMeeting()
        {
            var meetingDto = _page.PostMeeting();

            _driver.Click(_page.ExpandSection(_page.GetMeeting), _page.Submit(_page.GetMeeting));
            
            _page.IdParameterInput(_page.GetMeeting).SendKeys(meetingDto.Id);
            _driver.Click(_page.Submit(_page.GetMeeting), null ,"1000");

            var meetingId = _page.ResponseId(_page.GetMeeting).Text;
            var meetingDate = _page.ResponseDate(_page.GetMeeting).TextOnly();
            var meetingName = _page.ResponseMeetingName(_page.GetMeeting).TextOnly();
            var meetingStartTime = _page.ResponseMeetingStartTime(_page.GetMeeting).TextOnly(); 
            var meetingEndTime = _page.ResponseMeetingEndTime(_page.GetMeeting).TextOnly();
            var meetingDescription = _page.ResponseMeetingDescription(_page.GetMeeting).TextOnly();
            var meetingLocation = _page.ResponseMeetingLocation(_page.GetMeeting).TextOnly();

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
            var seriesId = meetingDto.MeetingSeriesId;
            var updatedMeetingDto = _page.PutMeeting(id, seriesId);
           
            _driver.Click(_page.ExpandSection(_page.GetMeeting), _page.Submit(_page.GetMeeting));

            _driver.ScrollToElement(_page.IdParameterInput(_page.GetMeeting));
            _page.IdParameterInput(_page.GetMeeting).SendKeys(meetingDto.Id);
            _driver.Click(_page.Submit(_page.GetMeeting), null, "1000");

            var meetingId = _page.ResponseId(_page.GetMeeting).TextOnly();
            var meetingDate = _page.ResponseDate(_page.GetMeeting).TextOnly();
            var meetingName = _page.ResponseMeetingName(_page.GetMeeting).TextOnly();
            var meetingStartTime = _page.ResponseMeetingStartTime(_page.GetMeeting).TextOnly();
            var meetingEndTime = _page.ResponseMeetingEndTime(_page.GetMeeting).TextOnly();
            var meetingDescription = _page.ResponseMeetingDescription(_page.GetMeeting).TextOnly();
            var meetingLocation = _page.ResponseMeetingLocation(_page.GetMeeting).TextOnly();

            Assert.That(meetingId, Is.EqualTo(updatedMeetingDto.Id));
            Assert.That(meetingDate, Is.EqualTo(updatedMeetingDto.Date));
            Assert.That(meetingName, Is.EqualTo(updatedMeetingDto.MeetingName));
            Assert.That(meetingStartTime, Is.EqualTo(updatedMeetingDto.StartTime));
            Assert.That(meetingEndTime, Is.EqualTo(updatedMeetingDto.EndTime));
            Assert.That(meetingDescription, Is.EqualTo(updatedMeetingDto.Description));
            Assert.That(meetingLocation, Is.EqualTo(updatedMeetingDto.Location));
        }
    }
}
 

    