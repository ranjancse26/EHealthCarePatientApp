using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDesktopApp.Helpers
{
    public class NameValuePairs
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public NameValuePairs(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public NameValuePairs()
        {
        }
    }
}
