using System.Security.Cryptography.X509Certificates;
using System.Threading;
using MeetingsIT2;
using NUnit.Framework;

namespace MeetingsTests.Api
{
    public class MeetingV2Tests : BaseTest<MeetingV2Page>.ApiBaseTest<MeetingV2Page>
    {
        protected override MeetingV2Page CreatePage()
        {
            return new MeetingV2Page(_driver);
        }

        [Test]
        public void Test()
        {
            Thread.Sleep(3000);
        }
    }
}
 

    