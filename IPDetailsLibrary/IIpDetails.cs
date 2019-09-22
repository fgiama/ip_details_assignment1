using System;
using System.Collections.Generic;
using System.Text;

namespace IPDetailsLibrary
{
    public interface IpDetails
    {
        string City { get; set; }
        string Country { get; set; }
        string Continent { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }

    }
}
