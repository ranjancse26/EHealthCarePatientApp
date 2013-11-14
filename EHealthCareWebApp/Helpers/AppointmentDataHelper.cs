using EHealthCareDataAccess;
using EHealthCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHealthCareWebApp.Helpers
{
    public class AppointmentDataDataHelper
    {
        List<AppointmentViewModel> appointmentViewModelList;
        string uniqueGuid;
        int patientId;

        public AppointmentDataDataHelper(string uniqueGuid, int patientId)
        {
            this.uniqueGuid = uniqueGuid;
            this.patientId = patientId;
            appointmentViewModelList = new List<AppointmentViewModel>();
        }

        public List<AppointmentViewModel> GetViewModel()
        {
            var providerDataRepository = new ProviderDataRepository();
            var apponitmentDataRepository = new AppointmentDataRepository(Guid.Parse(uniqueGuid));
            var appointments = apponitmentDataRepository.GetAllAppointmentData(this.patientId);
            foreach (var appointment in appointments)
            {
                appointmentViewModelList.Add(new AppointmentViewModel
                {
                    StartDate = appointment.StartDate,
                    EndDate = appointment.EndDate,
                    Notes = appointment.Notes,
                    ProviderName = providerDataRepository.GetProviderById(appointment.ProviderId),
                    Purpose = appointment.Purpose,
                    SpecialtyName = apponitmentDataRepository.GetSpecialtiyName(appointment.SpecialtyId),
                    Status = appointment.Status,
                    Type = appointment.Type 
                });
            }
            return appointmentViewModelList;
        }
    }
}