using EHealthCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace EHealthCareWebApp.Controllers
{
    public class PillIdentifierController : Controller
    {
        public ActionResult Index(string shape, string color, string ingredient)
        {
            List<PillInfoEntity> pillInfoEntities = new List<PillInfoEntity>();
            string url = string.Format("http://pillbox.nlm.nih.gov/PHP/pillboxAPIService.php?key=12345&shape={0}&color={1}&ingredient={2}",
                                        shape, color, ingredient);
            try
            {
                XDocument xmlDocument = XDocument.Load(url);

                pillInfoEntities = (from item in xmlDocument.Descendants("pill")
                                 select new PillInfoEntity
                                 {
                                     Author = item.Element("AUTHOR").Value,
                                     Color = item.Element("SPLCOLOR").Value,
                                     HasImage = item.Element("HAS_IMAGE").Value,
                                     Imprint = item.Element("SPLIMPRINT").Value,
                                     Ingredients = item.Element("INGREDIENTS").Value,
                                     NDC9 = item.Element("NDC9").Value,
                                     ProductCode = item.Element("PRODUCT_CODE").Value,
                                     RxCUI = item.Element("RXCUI").Value,
                                     RxString = item.Element("RXSTRING").Value,
                                     RxTTY = item.Element("RXTTY").Value,
                                     Score = item.Element("SPLSCORE").Value,
                                     Shape = item.Element("SPLSHAPE").Value,
                                     Size = item.Element("SPLSIZE").Value,
                                     ImageUrl = string.Format("http://pillbox.nlm.nih.gov/assets/small/{0}.jpg", item.Element("image_id").Value)
                                 }).ToList();
            }
            catch
            {

            }
            return View(pillInfoEntities);
        }
    }
}
