using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Models
{
    public class StepsDataViewEntity
    {
        public string Date { get; set; }
        public double Steps { get; set; }
    }

    public class StepsDataViewModel
    {
        public StepsDataViewModel()
        {
            this.StepsDataEntity = new List<StepsDataViewEntity>();
            this.StepsDataChart = new StepsDataViewForChart();
        }
        public List<StepsDataViewEntity> StepsDataEntity { get; set; }
        public StepsDataViewForChart StepsDataChart { get; set; }
    }

    public class StepsDataViewForChart
    {
        public string Labels { get; set; }
        public string StepsData { get; set; }
    }
}