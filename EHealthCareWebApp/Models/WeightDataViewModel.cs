using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Models
{
    public class WeightDataViewEntity
    {
        public string Date { get; set; }
        public double Weight { get; set; }
    }

    public class WeightDataViewModel
    {
        public WeightDataViewModel()
        {
            this.WeightDataEntity = new List<WeightDataViewEntity>();
            this.WeightDataChart = new WeightDataViewForChart();
            this.WeightRelatedItem = new WeightRelatedItems();
        }
        public List<WeightDataViewEntity> WeightDataEntity { get; set; }
        public WeightDataViewForChart WeightDataChart { get; set; }
        public WeightRelatedItems WeightRelatedItem { get; set; }
    }

    public class WeightRelatedItems
    {
        public double MostRecentWeight { get; set; }
        public double MinWeight { get; set; }
        public double LastMonthsChange { get; set; }
    }

    public class WeightDataViewForChart
    {
        public string Labels { get; set; }
        public string WeightData { get; set; }
        public string WeightGoalData { get; set; }
    }
}