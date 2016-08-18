using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Loggly
{
    internal class HttpClient : IHttpClient
    {
        private System.Net.Http.HttpClient _cli;

        public HttpClient()
        {
            _cli = new System.Net.Http.HttpClient();
        }

        public byte[] PostData(string url, string data)
        {
            HttpContent content = new ByteArrayContent(System.Text.Encoding.ASCII.GetBytes(data));
            content.Headers.Add("Keep-Alive", "false");  
            content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            var response = _cli.PostAsync(new Uri(url), content).GetAwaiter().GetResult();
            return response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
        }

        public byte[] PostData(string url, Event data)
        {

            var msg = JsonConvert.SerializeObject(data);
            return PostData(url, msg);

        }
    }
}
