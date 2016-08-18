using System;
using Microsoft.Extensions.Logging;

namespace Loggly.Extensions.Logging
{
    public static class LogglyLoggerFactoryExtensions
    {
        public static ILoggerFactory AddLoggly( this ILoggerFactory factory, string apiKey, string source = null )
        {
            return AddLoggly( factory, LogLevel.Information, apiKey, source );
        }

        public static ILoggerFactory AddLoggly( this ILoggerFactory factory, Func<string, LogLevel, EventId, bool> filter, string apiKey, string source = null )
        {
            factory.AddProvider( new LogglyLoggerProvider( filter, apiKey, source ) );
            return factory;
        }

        public static ILoggerFactory AddLoggly( this ILoggerFactory factory, LogLevel minLevel,string apiKey, string source = null )
        {
            return AddLoggly( factory, ( category, logLevel, eventId ) => logLevel >= minLevel, apiKey, source );
        }
    }
}
