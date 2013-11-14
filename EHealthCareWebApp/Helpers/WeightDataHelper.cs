using EHealthCareDataAccess;
using EHealthCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Helpers
{
    public class WeightDataHelper
    {
        WeightDataViewModel weightDataViewModel;
        string uniqueGuid;
        int patientId;

        public WeightDataHelper(string uniqueGuid, int patientId)
        {
            this.uniqueGuid = uniqueGuid;
            this.patientId = patientId;
            weightDataViewModel = new WeightDataViewModel();
        }

        private static string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }

        public WeightDataViewModel GetViewModel()
        {
            var weightDataRepository = new WeightDataRepository(Guid.Parse(uniqueGuid));
            var weightDataList = weightDataRepository.GetAllWeightData(patientId);
            double minWeight = 0;
            int prevMonth = DateTime.Now.Date.Month - 1;
            double prevMonthWeight = 0;

            foreach (var item in weightDataList)
            {
                weightDataViewModel.WeightDataEntity.Add(new WeightDataViewEntity
                {
                    Date = item.Date.ToShortDateString(),
                    Weight = item.Weight1
                });

                if(minWeight == 0)
                   minWeight = item.Weight1;

                if (item.Weight1 < minWeight)
                {
                    minWeight = item.Weight1;
                }

                if (item.Date.Month == prevMonth)
                    prevMonthWeight = item.Weight1;
            }

            if (weightDataViewModel.WeightDataEntity.Count > 0)
            {
                weightDataViewModel.WeightRelatedItem.MostRecentWeight = weightDataViewModel.WeightDataEntity[0].Weight;
                weightDataViewModel.WeightRelatedItem.MinWeight = minWeight;
                weightDataViewModel.WeightRelatedItem.LastMonthsChange = Math.Round(weightDataViewModel.WeightRelatedItem.MostRecentWeight - prevMonthWeight, 2);
            }

            // Get Labels
            string lables = "[";
            foreach (var item in weightDataList)
            {
                lables = lables + PutIntoQuotes(item.Date.ToShortDateString()) + ",";
            }
            lables = lables.Substring(0, lables.Length - 1) + "]";

            weightDataViewModel.WeightDataChart.Labels = lables.Replace(@"\", " ");

            // Weight Data
            string weightData = "[";
            string weightGoalData = "[";
            
            foreach (var item in weightDataList)
            {
                weightData = weightData + item.Weight1.ToString() + ",";
                weightGoalData = weightGoalData + item.WeightGoal.ToString() + ",";
            }

            weightData = weightData.Substring(0, weightData.Length - 1) + "]";
            weightGoalData = weightGoalData.Substring(0, weightGoalData.Length - 1) + "]";

            weightDataViewModel.WeightDataChart.WeightData = weightData;
            weightDataViewModel.WeightDataChart.WeightGoalData = weightGoalData;

            return weightDataViewModel;
        }
    } 
}