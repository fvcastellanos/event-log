
using System.Collections.Generic;
using EventLog.Model;
using System.IO;
using Microsoft.Extensions.Logging;

namespace EventLog.Dao
{
    public class EventDataDao
    {
        private static string DefaultFileName = "event-data.txt";

        private ILogger _logger;

        private string _fileName;

        public EventDataDao(ILogger<EventDataDao> logger)
        {
            _fileName = DefaultFileName;
            _logger = logger;
        }

        // public EventDataDao(string fileName)
        // {
        //     _fileName = fileName;
        // }

        public void saveEvent(EventData eventData)
        {
            using (StreamWriter writer = File.AppendText(_fileName))
            {
                writer.WriteLine(eventData.ToString());
            }
        }

        public IList<EventData> getEvents() 
        {
            return null;
        }
    }
}