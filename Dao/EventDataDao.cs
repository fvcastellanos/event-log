
using System.Collections.Generic;
using EventLog.Model;
using System.IO;
using Microsoft.Extensions.Logging;
using System;

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

        public void saveEvent(EventData eventData)
        {
            using (StreamWriter writer = File.AppendText(_fileName))
            {
                _logger.LogInformation("Writing evd: {0} in file: {1}", eventData.ToString(), _fileName);
                writer.WriteLine(eventData.ToString());
            }
        }

        public IList<EventData> GetEvents() 
        {
            var events = new List<EventData>();
            using (StreamReader reader = File.OpenText(_fileName)) 
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    events.Add(convertToEventData(line));
                }
            }

            return events;
        }

        private EventData convertToEventData(string line)
        {
            var tokens = line.Split('|');
            var eventData = new EventData();
            eventData.User = tokens[0];
            eventData.Password = tokens[1];
            eventData.Ip = tokens[2];
            eventData.EventDate = DateTime.Parse(tokens[3]);
            eventData.UserAgent = tokens[4];

            return eventData;
        }
    }
}