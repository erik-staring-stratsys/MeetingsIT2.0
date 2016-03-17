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
    public class UserV2Tests : BaseTest<UserV2Page>.ApiUserV2BaseTest<UserV2Page>
    {
        protected override UserV2Page CreatePage()
        {
            return new UserV2Page(_driver);
        }

        
    }
}
 

    