using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Models
{
    public class AppointmentViewModel
    {
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string ProviderName { get; set; }
        public string SpecialtyName { get; set; }
        public string Purpose { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}