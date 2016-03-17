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
    public class UserV2Tests : BaseTest<UserV2Page>.ApiUserV2BaseTest<UserV2Page>
    {
        protected override UserV2Page CreatePage()
        {
            return new UserV2Page(_driver);
        }

        [Test]
        public void GetCurrentUser()
        {
            _driver.Click(_page.Submit(_page.GetCurrentUser), null, "1000");
            Assert.That(_page.ResponseName(_page.ResponseBody).TextOnly(), Is.EqualTo("Stratsysseleniumtests"));
            Assert.That(_page.ResponseEmail(_page.ResponseBody).TextOnly(), Is.EqualTo("stratsysseleniumtests@gmail.com"));
            Assert.That(_page.ResponseId(_page.ResponseBody).TextOnly(), Is.EqualTo("1177"));
            Assert.That(_page.ResponseIsExternal(_page.ResponseBody).TextOnly(), Is.EqualTo("false"));
            Assert.That(_page.ResponseInitials(_page.ResponseBody).TextOnly(), Is.EqualTo("S"));
            Assert.That(_page.ResponseGravatarId(_page.ResponseBody).TextOnly(), Is.EqualTo("9224cb85919d0ddcdef3e6e4fa7f15c2"));
            Assert.That(_page.ResponseLabel(_page.ResponseBody).TextOnly(), Is.EqualTo("stratsysseleniumtests@gmail.com"));
            Assert.That(_page.ResponseHasFinishedMeetingGuide(_page.ResponseBody).TextOnly(), Is.EqualTo("true"));
            Assert.That(_page.ResponseHasAcceptedTerms(_page.ResponseBody).TextOnly(), Is.EqualTo("true"));
            Assert.That(_page.ResponseHasFinishedIntroGuide(_page.ResponseBody).TextOnly(), Is.EqualTo("true"));
            Assert.That(_page.ResponseCanAddUsers(_page.ResponseBody).TextOnly(), Is.EqualTo("true"));
            Assert.That(_page.ResponseIsOrganizerForAnyMeeting(_page.ResponseBody).TextOnly(), Is.EqualTo("true"));
            Assert.That(_page.ResponseIsAdmin(_page.ResponseBody).TextOnly(), Is.EqualTo("true"));
        }
    }
}
 

    