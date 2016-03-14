using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsTests.Configs
{
    public class Config
    {
        public static string BaseUrl
        {
            get
            {
#if TEST
                return "https://test.runyourmeeting.com/";
#else
                return "https://staging.runyourmeeting.com/";
#endif
            }
        }
    }
}
