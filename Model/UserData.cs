
using System.Collections.Generic;

namespace EventLog.Model
{
    public class UserData
    {
        public UserData(IList<EventInformation> eventsList)
        {
            EventsList = eventsList;
            HasAlert = false;
        }

        public IList<EventInformation> EventsList { get; }

        public bool HasAlert { get; set; }
    }
}