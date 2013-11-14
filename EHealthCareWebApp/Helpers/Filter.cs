using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HealthCareProvider.Models
{
    public class Filter
    {      
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Category { get; set; }
        public string Locality { get; set; }
        public string Insurances { get; set; }
        public string Npi { get; set; }
        public string Region { get; set; }   
    }
}