﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Loggly
{
    /// <summary>
    /// Allows applications to post events and track users with Loggly
    /// </summary>
    public class LogClient
    {
        public BlockingCollection<Event> LogMessages { get; set; }
        
        private IHttpClient HttpClient { get; set; }

        public LogClient(string apiKey)
        {
            LogMessages= new BlockingCollection<Event>();
            ApiKey = apiKey;
            HttpClient = new HttpClient();
            Task.Factory.StartNew(ProcessMessages);
        }

        private void ProcessMessages()
        {
            while (!Environment.HasShutdownStarted)
            {
                Event msg;
                LogMessages.TryTake(out msg, 1000);
                if (msg == null) continue;
                SendEvent(msg);
            }
        }

        private void SendEvent(Event msg)
        {
            HttpClient.PostData($"http://logs-01.loggly.com/inputs/{ApiKey}/tag/http,listenercore/", msg);
        }

        public string ApiKey { get; set; }

        public void Post(Event logEvent)
        {
            LogMessages.TryAdd(logEvent);
        }
    }
}
