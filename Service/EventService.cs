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
            var geoIp = _freeGeoIpClient.GetGeoIpInformation("cavitos.net");
        }
    }
}