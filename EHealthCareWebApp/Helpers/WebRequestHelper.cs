using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace EHealthCareWebApp.Helpers
{
    public class WebRequestHelper
    {
        public XDocument GetXmlResponse(string url, WebHeaderCollection headerCollection = null)
        {
            var client = new WebClient();
            if (headerCollection != null)
                client.Headers = headerCollection;
            var response = client.DownloadData(new Uri(url));

            TextReader tr = new StringReader(System.Text.Encoding.UTF8.GetString(response));
            XDocument doc = XDocument.Load(tr);
            return doc;
        }

        public string GetJsonResponse(string url, WebHeaderCollection headerCollection = null)
        {
            var client = new WebClient();
            if (headerCollection != null)
                client.Headers = headerCollection;
            var response = client.DownloadData(new Uri(url));
            return System.Text.Encoding.Default.GetString(response);
        }
    }
}