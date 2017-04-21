using System.Collections.Generic;
using EventLog.Dao;
using EventLog.FreeGeoIp;
using EventLog.Model;
using Microsoft.Extensions.Logging;

// http://freegeoip.net/?q=190.149.19.62

namespace EventLog.Service
{
    public class EventService
    {
        private ILogger _logger;

        private EventDataDao _eventDataDao;

        private FreeGeoIpClient _freeGeoIpClient;

        public EventService(ILogger<EventService> logger, EventDataDao eventDataDao, FreeGeoIpClient freeGeoIpClient)
        {
            _logger = logger;
            _eventDataDao = eventDataDao;
            _freeGeoIpClient = freeGeoIpClient;
        }

        public void LogEvent(EventData eventData)
        {
            _logger.LogInformation(eventData.ToString());
            _eventDataDao.saveEvent(eventData);
        }

        public IDictionary<string, UserData> GetEvents()
         {
            var events = _eventDataDao.GetEvents();
            var userEventMap = BuildUserDataMap(events);

            return userEventMap;
        }

        private UserData GetUserData(string user, IDictionary<string, UserData> userDataMap)
        {
            if (!userDataMap.ContainsKey(user))
            {
                userDataMap[user] = new UserData(new List<EventInformation>());
            }

            return userDataMap[user];
        }

        private IDictionary<string, UserData> BuildUserDataMap(IList<EventData> events) 
        {
            var userMap = new Dictionary<string, UserData>();

            foreach (EventData e in events)
            {
                var geoIpTask = _freeGeoIpClient.GetGeoIpInformation(e.Ip);
                var eventInformation = new EventInformation(e, geoIpTask.Result);
                
                var userData = GetUserData(e.User, userMap);
                userData.EventsList.Add(eventInformation);
            }

            return userMap;
        }
    }
}