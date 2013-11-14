using EHealthCareWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHealthCareWebApp.Controllers
{
    public class DrugController : Controller
    {
        public ActionResult DrugSearchByName(string id)
        {
            try
            {
                string HealthDataInitiativeApiKey = ConfigurationManager.AppSettings["HealthDataInitiativeApiKey"].ToString();
                string url = string.Format("https://api.carepass.com/hhs-directory-api/drugs/search?name={0}&apikey={1}", id, HealthDataInitiativeApiKey);
                var webRequestClient = new WebRequestHelper();
                ViewBag.DrugResult = webRequestClient.GetJsonResponse(url);
            }
            catch
            {
                ViewBag.DrugResult = "";
            }
            return View();
        }

        public ActionResult DrugSearchByNDC(string id)
        {
            try
            {
                string HealthDataInitiativeApiKey = ConfigurationManager.AppSettings["HealthDataInitiativeApiKey"].ToString();
                string url = string.Format("https://api.carepass.com/hhs-directory-api/drugs/{0}?apikey={1}", id, HealthDataInitiativeApiKey);
                var webRequestClient = new WebRequestHelper();
                ViewBag.DrugResult = webRequestClient.GetJsonResponse(url);
            }
            catch
            {
                ViewBag.DrugResult = "";
            }
            return View();
        }

        public ActionResult DrugSearchByNDA(string id)
        {
            try
            {
                string HealthDataInitiativeApiKey = ConfigurationManager.AppSettings["HealthDataInitiativeApiKey"].ToString();
                string url = string.Format("https://api.carepass.com/hhs-directory-api/applications/{0}?apikey={1}", id, HealthDataInitiativeApiKey);
                var webRequestClient = new WebRequestHelper();
                ViewBag.DrugResult = webRequestClient.GetJsonResponse(url);
            }
            catch
            {
                ViewBag.DrugResult = "";
            }
            return View();
        }
    }
}
