
namespace EventLog.Model
{
    public class EventInformation
    {
        public EventInformation(EventData eventData, GeoIp geoIp)
        {
            Event = eventData;
            GeoInfo = geoIp;
        }

        public EventData Event { get; }
        public GeoIp GeoInfo { get; }
    }
}