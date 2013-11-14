using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Models
{
    public class PillInfoEntity
    {
        public string ProductCode { get; set; }
        public string NDC9 { get; set; }
        public string Color { get; set; }
        public string Imprint { get; set; }
        public string Shape { get; set; }
        public string Size { get; set; }
        public string Score { get; set; }
        public string RxCUI { get; set; }
        public string RxTTY { get; set; }
        public string RxString { get; set; }
        public string Ingredients { get; set; }
        public string HasImage { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
    }
}