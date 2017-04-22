using System;
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

        private CryptoService _cryptoService;

        public EventService(ILogger<EventService> logger, EventDataDao eventDataDao, FreeGeoIpClient freeGeoIpClient, CryptoService cryptoService)
        {
            _logger = logger;
            _eventDataDao = eventDataDao;
            _freeGeoIpClient = freeGeoIpClient;
            _cryptoService = cryptoService;
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
                // var geoIpTask = _freeGeoIpClient.GetGeoIpInformation(e.Ip);
                var geoIpTask = _freeGeoIpClient.GetGeoIpInformation(GetRandomDomain());
                e.Password = _cryptoService.DecryptTextAes(e.Password);
                var eventInformation = new EventInformation(e, geoIpTask.Result);
                
                var userData = GetUserData(e.User, userMap);
                userData.EventsList.Add(eventInformation);
                VerifyAlert(userData);
            }

            return userMap;
        }

        private void VerifyAlert(UserData userData)
        {
            EventData old = null;
            var change = 1;
            foreach (EventInformation info in userData.EventsList)
            {
                if(old == null)
                {
                    old = info.Event;
                }
                else
                {
                    if (!old.UserAgent.Equals(info.Event.UserAgent) && (old.EventDate.Subtract(info.Event.EventDate).Hours < 1))
                    {
                        change++;
                        old = info.Event;
                    }
                }
            }

            if(change >= 5)
            {
                userData.HasAlert = true;
            }
        }
        private string GetRandomDomain()
        {
            var domainList = new List<string>();

            domainList.Add("cavitos.net");
            domainList.Add("wikipedia.org");
            domainList.Add("google.com");
            domainList.Add("umg.edu.gt");
            domainList.Add("usac.edu.gt");
            domainList.Add("microsoft.com");
            domainList.Add("facebook.com");
            domainList.Add("amazon.com");
            domainList.Add("gmail.com");
            domainList.Add("xibalbanetwork.com");
            domainList.Add("gog.com");
            domainList.Add("spotify.com");
            domainList.Add("movistar.com");

            Random d = new Random();
            var index = d.Next(0, domainList.Count - 1);

            return domainList[index];
        }
    }
}