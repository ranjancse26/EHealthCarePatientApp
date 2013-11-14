using FactualDriver;
using FactualDriver.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HealthCareProvider.Models
{
    public class HealthCareProviderRepository
    {
        public Factual _factual;

        public HealthCareProviderRepository()
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["FactualAuthKey"]) ||
                string.IsNullOrEmpty(ConfigurationManager.AppSettings["FactualAuthSecret"]))
                throw new ConfigurationErrorsException("Missing Factual API keys in your web.config.");

            _factual = new Factual(ConfigurationManager.AppSettings["FactualAuthKey"], ConfigurationManager.AppSettings["FactualAuthSecret"]);
        }

        public string GetHealthCareProviderData(ProviderFilter filter)
        {
            try
            {
                var query = new Query();
      
                if (filter.Latitude != 0 && filter.Longitude != 0)
                {
                    query.WithIn(new Circle(filter.Latitude, filter.Longitude, filter.Meters));
                }

                if (!string.IsNullOrWhiteSpace(filter.Category))
                {
                    query.Field("category").Search(filter.Category);
                }

                if (!string.IsNullOrWhiteSpace(filter.Npi))
                {
                    query.Field("npi_id").Search(filter.Npi);
                }

                if (!string.IsNullOrWhiteSpace(filter.Insurances))
                {
                    query.Field("insurances").Search(filter.Insurances);
                }

                if (!string.IsNullOrWhiteSpace(filter.Locality))
                {
                    query.Field("locality").Search(filter.Locality);
                }

                if (!string.IsNullOrWhiteSpace(filter.Region))
                {
                    query.Field("region").Search(filter.Region);
                }

                query.Field("offset").Equals(0);

                return _factual.Fetch("health-care-providers-us", query);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}