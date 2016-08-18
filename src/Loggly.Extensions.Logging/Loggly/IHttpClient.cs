using System;
using System.Collections.Generic;
using System.Text;

namespace Loggly
{
    public interface IHttpClient
    {
        byte[] PostData(string url, string data);
        byte[] PostData(string url, Event data);
    }
}
