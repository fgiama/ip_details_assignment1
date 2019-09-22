using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IPDetailsLibrary
{
    internal class IpInfoProvider : IIpInfoProvider
    {
        private readonly string BaseUri = "http://api.ipstack.com/{0}?access_key={1}";
        private static readonly HttpClient _client;

        static IpInfoProvider()
        {
            _client = new HttpClient();
        }
        public IpDetails GetDetails(string ip)
        {
            try
            {
                var task = GetIpDetails(ip);
                var awaiter = task.GetAwaiter();
                return awaiter.GetResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IpDetails> GetIpDetails(string ip)
        {
            var apiKey = ConfigurationManager.AppSettings["apiKey"];
            var uri = string.Format(BaseUri, ip, apiKey);
            HttpResponseMessage response = await _client.GetAsync(uri).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return ConvertResponseBody(responseBody);
        }

        private IpDetails ConvertResponseBody(string response)
        {
            IpDetailsItem ipDetails = null;
            try
            {
                ipDetails = JsonConvert.DeserializeObject<IpDetailsItem>(response);
            }
            catch
            {
                throw new Exception("IP not found.");
            }

            if (ipDetails.Success.HasValue && !ipDetails.Success.Value)
                throw new Exception(ipDetails.Error.Info);

            return ipDetails;
        }

    }
}
