using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareProvider.Models
{
    interface IRepository
    {
        string GetHealthCareProviderData(Filter filter);   
    }
}
