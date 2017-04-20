
using System;

namespace EventLog.Model
{
    public class EventData
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public DateTime EventDate { get; set; }

    }
}