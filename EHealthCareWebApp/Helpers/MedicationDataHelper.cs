using EHealthCareDataAccess;
using EHealthCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Helpers
{
    public class MedicationDataHelper
    {
        List<MedicationViewModel> medicationViewModelList;
        string uniqueGuid;
        int patientId;

        public MedicationDataHelper(string uniqueGuid, int patientId)
        {
            this.uniqueGuid = uniqueGuid;
            this.patientId = patientId;
            medicationViewModelList = new List<MedicationViewModel>();
        }

        public List<MedicationViewModel> GetViewModel()
        {
            var medicationDataRepository = new MedicationDataRepository(Guid.Parse(uniqueGuid));
            var medications = medicationDataRepository.GetAllMedicationData(this.patientId);
            foreach (var medication in medications)
            {
                medicationViewModelList.Add(new MedicationViewModel
                {
                    MedicationName = medication.MedicationName,
                    StartDate = medication.StartDate.ToShortDateString(),
                    EndDate = medication.EndDate == null ? "" : medication.EndDate.ToString(),
                    HowTaken = medication.HowTaken,
                    Dose = medication.DoseText + " " + medication.DoseUnit,
                    Strength = medication.StrengthText + " " + medication.StrengthUnit,
                    ReasonForTaking = medication.ReasonForTaking 
                });
            }
            return medicationViewModelList;
        }
    }
}