
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using EventLog.Model;

namespace EventLog.FreeGeoIp
{
    public class FreeGeoIpClient
    {
        private static string FreeIpUri = "http://freegeoip.net/json/";
        private HttpClient _client;

        public FreeGeoIpClient()
        {
            _client = new HttpClient();
        }

        public async Task<GeoIp> GetGeoIpInformation(string domain)
        {
            var serializer = new DataContractJsonSerializer(typeof(GeoIp));

            string url = FreeIpUri + domain;
            EventLog.Model.GeoIp geoIp = null;
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var streamTask = response.Content.ReadAsStreamAsync();
                geoIp = serializer.ReadObject(await streamTask) as GeoIp;
            }
            return geoIp;
        }        
    }
}