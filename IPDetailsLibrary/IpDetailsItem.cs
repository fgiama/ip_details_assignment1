using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPDetailsLibrary
{
    //internal class IpDetailsItem : IpDetails
    //{
    //    public string City { get; set; }
    //    public string Country { get; set; }
    //    public string Continent { get; set; }
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //}

    [JsonObject]
    internal class IpDetailsItem : IpDetails
    {
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country_name")]
        public string Country { get; set; }
        [JsonProperty("continent_name")]
        public string Continent { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        [JsonProperty("success")]
        public bool? Success { get; set; }
        [JsonProperty("error")]
        public IpDetailsError Error { get; set; }
        public IpDetailsItem() { }
    }

    [JsonObject]
    internal class IpDetailsError
    {
        [JsonProperty("info")]
        public string Info { get; set; }
    }
}
