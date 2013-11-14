using Newtonsoft.Json.Linq;
using EHealthCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EHealthCareWebApp.Controllers
{
    public class HealthCareProviderController : Controller
    {
        [HttpGet]
        public ActionResult GetAllProviders(HealthCareProvider.Models.ProviderFilter filter)
        {
            var providers = GetFilteredProviders(filter);
            return PartialView("ProvidersWebGrid", providers);
        }

        private List<Provider> GetFilteredProviders(HealthCareProvider.Models.ProviderFilter filter)
        {
            var repository = new HealthCareProvider.Models.HealthCareProviderRepository();
            var providers = new List<Provider>();

            var data = repository.GetHealthCareProviderData(filter);
            JObject json = JObject.Parse(data);
            var jss = new JavaScriptSerializer();
            dynamic dynamicData = jss.Deserialize<dynamic>(json["response"]["data"].ToString());

            for (int i = 0; i < dynamicData.Length; i++)
            {
                var item = (dynamicData[i] as System.Collections.Generic.Dictionary<string, object>);
                var provider = new Provider();
                if (item.ContainsKey("name"))
                {
                    provider.Name = item["name"].ToString();
                }
                if (item.ContainsKey("locality"))
                {
                    provider.Locality = item["locality"].ToString();
                }
                if (item.ContainsKey("latitude"))
                {
                    provider.Latitude = item["latitude"].ToString();
                }
                if (item.ContainsKey("longitude"))
                {
                    provider.Longitude = item["longitude"].ToString();
                }
                if (item.ContainsKey("npi_id"))
                {
                    provider.Npi = item["npi_id"].ToString();
                }
                if (item.ContainsKey("address"))
                {
                    provider.Address = item["address"].ToString();
                }
                if (item.ContainsKey("region"))
                {
                    provider.Region = item["region"].ToString();
                }
                providers.Add(provider);
            }

            return providers;
        }
    }
}
