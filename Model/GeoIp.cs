
using System.Runtime.Serialization;

namespace EventLog.Model
{
    public class GeoIp
    {
        public string ip { get; set; }
        public string country_code { get; set; }

        public string country_name { get; set; }

        public string region_code { get; set; }

        public string region_name { get; set; }

        public string zip_code { get; set; }

        public string time_zone { get; set; }

        public double latitude { get; set; }

        public double longitude { get; set; }

        public string metro_code { get; set; }

    }
}