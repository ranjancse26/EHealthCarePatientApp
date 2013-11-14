using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCareProvider.Models
{
    public class ProviderFilter
    {      
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Category { get; set; }
        public string Locality { get; set; }
        public string Insurances { get; set; }
        public string Npi { get; set; }
        public string Region { get; set; }
        public int Meters { get; set; }
    }
}