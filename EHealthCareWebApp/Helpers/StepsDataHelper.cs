using EHealthCareDataAccess;
using EHealthCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Helpers
{
    public class StepsDataHelper
    {
        StepsDataViewModel stepsDataViewModel;
        string uniqueGuid;
        int patientId;

        public StepsDataHelper(string uniqueGuid, int patientId)
        {
            this.uniqueGuid = uniqueGuid;
            this.patientId = patientId;
            stepsDataViewModel = new StepsDataViewModel();
        }

        private static string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }

        public StepsDataViewModel GetViewModel()
        {
            var stepsDataRepository = new StepsDataRepository(Guid.Parse(uniqueGuid));
            var stepsDataList = stepsDataRepository.GetAllStepsData(patientId);

            foreach (var item in stepsDataList)
            {
                stepsDataViewModel.StepsDataEntity.Add(new StepsDataViewEntity
                {
                    Date = item.Date.ToShortDateString(),
                    Steps = item.Steps 
                });
            }

            // Get Labels
            string lables = "[";
            foreach (var item in stepsDataList)
            {
                lables = lables + PutIntoQuotes(item.Date.ToShortDateString()) + ",";
            }
            lables = lables.Substring(0, lables.Length - 1) + "]";

            stepsDataViewModel.StepsDataChart.Labels = lables.Replace(@"\", " ");

            // Steps Data
            string stepsData = "[";
            foreach (var item in stepsDataList)
            {
                stepsData = stepsData + item.Steps.ToString() + ",";
            }
            stepsData = stepsData.Substring(0, stepsData.Length - 1) + "]";
            stepsDataViewModel.StepsDataChart.StepsData = stepsData;

            return stepsDataViewModel;
        }
    }
}