using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using EHealthCareWebApp.Helpers;
using EHealthCareWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace EHealthCareWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
     
        /// <summary>
        /// Weight Action
        /// </summary>
        /// <param name="uniqueGuid">Unique GUID</param>
        /// <param name="patientId">Patient ID</param>
        /// <returns></returns>
        public ActionResult Weight(string uniqueGuid, int patientId)
        {
            WeightDataViewModel weightDataViewModel = new WeightDataViewModel();
            try
            {
                weightDataViewModel = new WeightDataHelper(uniqueGuid, patientId).GetViewModel();
                ViewBag.Error = "";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(weightDataViewModel);
        }

        /// <summary>
        /// Steps Action
        /// </summary>
        /// <param name="uniqueGuid">Unique GUID</param>
        /// <param name="patientId">Patient ID</param>
        /// <returns></returns>
        public ActionResult Steps(string uniqueGuid, int patientId)
        {
            StepsDataViewModel stepsDataViewModel = new StepsDataViewModel();
            try
            {
                stepsDataViewModel = new StepsDataHelper(uniqueGuid, patientId).GetViewModel();
                ViewBag.Error = "";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(stepsDataViewModel);
        }

        /// <summary>
        /// Medication Action
        /// </summary>
        /// <param name="uniqueGuid">Unique GUID</param>
        /// <param name="patientId">Patient ID</param>
        /// <returns></returns>
        public ActionResult Medication(string uniqueGuid, int patientId)
        {
            List<MedicationViewModel> medicationsList = new List<MedicationViewModel>();
            try
            {
                var medicationDataHelper = new MedicationDataHelper(uniqueGuid, patientId);
                medicationsList = medicationDataHelper.GetViewModel();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(medicationsList);
        }

        /// <summary>
        /// Appointment Action
        /// </summary>
        /// <param name="uniqueGuid">Unique GUID</param>
        /// <param name="patientId">Patient ID</param>
        /// <returns></returns>
        public ActionResult Appointment(string uniqueGuid, int patientId)
        {
            List<AppointmentViewModel> appointmentsList = new List<AppointmentViewModel>();
            try
            {
                var appointmentDataHelper = new AppointmentDataDataHelper(uniqueGuid, patientId);
                appointmentsList = appointmentDataHelper.GetViewModel();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(appointmentsList);
        }

        /// <summary>
        /// BloodPressure Action
        /// </summary>
        /// <param name="uniqueGuid">Unique GUID</param>
        /// <param name="patientId">Patient ID</param>
        /// <returns></returns>
        public ActionResult BloodPressure(string uniqueGuid, int patientId)
        {
            BloodPressureDataViewModel bloodPressureViewModel = new BloodPressureDataViewModel();
            try
            {
                bloodPressureViewModel = new BloodPressureDataHelper(uniqueGuid, patientId).GetViewModel();
                ViewBag.Error = "";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(bloodPressureViewModel);
        }
    }
}
