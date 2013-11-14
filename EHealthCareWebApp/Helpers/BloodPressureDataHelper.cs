using EHealthCareDataAccess;
using EHealthCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Helpers
{
    public class BloodPressureDataHelper
    {
        BloodPressureDataViewModel bloodPressureDataViewModel;
        string uniqueGuid;
        int patientId;

        public BloodPressureDataHelper(string uniqueGuid, int patientId)
        {
            this.uniqueGuid = uniqueGuid;
            this.patientId = patientId;
            bloodPressureDataViewModel = new BloodPressureDataViewModel();
        }

        private static string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }

        public BloodPressureDataViewModel GetViewModel()
        {
            var bloodPressureDataRepository = new BloodPressureDataRepository(Guid.Parse(uniqueGuid));
            var bloodPressureDataList = bloodPressureDataRepository.GetAllBloodPressureData(patientId);
            foreach (var item in bloodPressureDataList)
            {
                bloodPressureDataViewModel.BloodPressureDataEntity.Add(new BloodPressureDataViewEntity
                {
                    Date = item.When.ToShortDateString(),
                    Diastolic = item.Diastolic,
                    Systolic = item.Systolic,
                    Pulse = item.Pulse,
                    IrregularHeartbeat = item.IrregularHeartbeat.Value  
                });
            }

            if (bloodPressureDataList.Count > 0)
            {
                bloodPressureDataViewModel.BloodPressureRelatedItem.MostRecentDiastolic = bloodPressureDataList[0].Diastolic;
                bloodPressureDataViewModel.BloodPressureRelatedItem.MostRecentSystolic = bloodPressureDataList[0].Systolic;
            }

            // Get Labels
            string lables = "[";
            foreach (var item in bloodPressureDataList)
            {
                lables = lables + PutIntoQuotes(item.When.ToShortDateString()) + ",";
            }
            lables = lables.Substring(0, lables.Length - 1) + "]";

            bloodPressureDataViewModel.BloodPressureDataChart.Labels = lables.Replace(@"\", " ");

            // Systolic Data
            string systolicData = "[";
            foreach (var item in bloodPressureDataList)
            {
                systolicData = systolicData + item.Systolic.ToString() + ",";
            }
            systolicData = systolicData.Substring(0, systolicData.Length - 1) + "]";
            bloodPressureDataViewModel.BloodPressureDataChart.SystolicData = systolicData;

            // Diastolic Data
            string diastolicData = "[";
            foreach (var item in bloodPressureDataList)
            {
                diastolicData = diastolicData + item.Diastolic.ToString() + ",";
            }
            diastolicData = diastolicData.Substring(0, diastolicData.Length - 1) + "]";
            bloodPressureDataViewModel.BloodPressureDataChart.DiastolicData = diastolicData;

            // Pulse Data
            string pulseData = "[";
            foreach (var item in bloodPressureDataList)
            {
                pulseData = pulseData + item.Pulse.ToString() + ",";
            }
            pulseData = pulseData.Substring(0, pulseData.Length - 1) + "]";
            bloodPressureDataViewModel.BloodPressureDataChart.PulseData = pulseData;
            return bloodPressureDataViewModel;
        }
    }
}