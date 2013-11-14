using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Models
{
    public class Provider
    {
        public string Address { get; set; }
        public string Affiliations { get; set; }
        public string Category { get; set; }
        public string Education { get; set; }
        public string Gender { get; set; }
        public string Insurances { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Locality { get; set; }
        public string Name { get; set; }
        public string Npi { get; set; }
        public string PostCode { get; set; }
        public string Region { get; set; }
        public string Telephone { get; set; }
        public double Experience { get; set; }
    }
}