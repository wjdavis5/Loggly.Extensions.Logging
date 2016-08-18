using System;
using Microsoft.Extensions.Logging;

namespace Loggly.Extensions.Logging
{
    public class LogglyLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, EventId, bool> m_filter;
        private readonly LogClient m_logClient;
        private readonly string m_source;

        public LogglyLoggerProvider( Func<string, LogLevel, EventId, bool> filter, string apiKey, string source = null )
        {
            m_filter = filter;
            ApiKey = apiKey;
            m_logClient = new LogClient(apiKey);
            m_source = source;
        }

        public string ApiKey { get; set; }

        public ILogger CreateLogger( string name )
        {
            return new LogglyLogger( m_logClient, name, m_source, m_filter,ApiKey );
        }

        public void Dispose()
        {
        }
    }
}
