using System;
using System.Collections.Generic;
using System.Text;

namespace IPDetailsLibrary
{
    public interface IIpInfoProvider
    {
        IpDetails GetDetails(string ip);
    }
}
