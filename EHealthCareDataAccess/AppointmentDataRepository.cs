using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class AppointmentDataRepository
    {
        Guid uniqueGuid;

        public AppointmentDataRepository()
        {
            
        }

        public AppointmentDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public Guid GetPatientUniqueIdentifier(int patientId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                var appointment = dataContext.Appointments.FirstOrDefault(a => a.PatientId == patientId);
                return appointment == null ? Guid.Empty : appointment.UniqueIdentifier;
            }
        }

        public List<Specialty> GetAllSpecialties()
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Specialties.ToList();
            }
        }

        public string GetSpecialtiyName(int specialtyId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                var specialty = dataContext.Specialties.FirstOrDefault(s => s.Id == specialtyId);
                return specialty == null ? "" : specialty.SpecialityName;
            }
        }

        public Appointment GetAppointmentById(int patientID, int appointmentId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Appointments.Where(i => i.Id == appointmentId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public List<Appointment> GetAllAppointmentData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return (from appointment in dataContext.Appointments
                        where appointment.PatientId == patientID && appointment.UniqueIdentifier == uniqueGuid
                        orderby appointment.StartDate descending
                        select appointment).ToList();
            }
        }

        public void DeleteAppointment(int patientID, int appointmentId)
        {
            try
            {
                var appointment = GetAppointmentById(patientID, appointmentId);
                if (appointment != null)
                {
                    using (var dataContext = new eHealthCareEntities())
                    {
                        dataContext.Appointments.Attach(appointment);
                        dataContext.Appointments.Remove(appointment);
                        dataContext.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ex.EntityValidationErrors.GetValidationErrors());
            }
            catch
            {
                throw;
            }
        }

        public void SaveAppointment(Appointment appointment)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    appointment.UniqueIdentifier = uniqueGuid;
                    dataContext.Appointments.Add(appointment);
                    dataContext.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.EntityValidationErrors.GetValidationErrors());
                }
                catch
                {
                    throw;
                }
            }
        }

        public void UpdateAppointmentStatus(int appointmentId, int patientID, string status)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var appoinmentToUpdate = GetAppointmentById(patientID, appointmentId);
                    if (appoinmentToUpdate != null)
                    {
                        appoinmentToUpdate.Status = status;
                        dataContext.Entry(appoinmentToUpdate).State = EntityState.Modified;
                        dataContext.SaveChanges();
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.EntityValidationErrors.GetValidationErrors());
                }
                catch
                {
                    throw;
                }
            }
        }

        public void UpdateAppointment(int appointmentId, Appointment appointment)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var appoinmentToUpdate = GetAppointmentById(appointment.PatientId, appointmentId);
                    if (appoinmentToUpdate != null)
                    {
                        appoinmentToUpdate.ProviderId = appointment.ProviderId;
                        appoinmentToUpdate.Purpose = appointment.Purpose;
                        appoinmentToUpdate.SpecialtyId = appointment.SpecialtyId;
                        appoinmentToUpdate.StartDate = appointment.StartDate;
                        appoinmentToUpdate.Type = appointment.Type;
                        appoinmentToUpdate.EndDate = appointment.EndDate;
                        dataContext.Entry(appoinmentToUpdate).State = EntityState.Modified;
                        dataContext.SaveChanges();
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.EntityValidationErrors.GetValidationErrors());
                }
                catch
                {
                    throw;
                }
            }
        }

        #region "****** Patient Audio Appointments ****


        public void Upload(AudioAppointment audioAppointment)
        {
            try
            {
                using (var dataContext = new eHealthCareEntities())
                {
                    audioAppointment.UniqueIdentifier = this.uniqueGuid;
                    dataContext.AudioAppointments.Add(audioAppointment);
                    dataContext.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ex.EntityValidationErrors.GetValidationErrors());
            }
            catch
            {
                throw;
            }
        }

        private AudioAppointment GetAudioAppointment(int patientID, int appointmentId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return (from audioAppointment in dataContext.AudioAppointments
                        where audioAppointment.PatientId == patientID && audioAppointment.UniqueIdentifier == uniqueGuid
                        orderby audioAppointment.DateTime descending
                        select audioAppointment).FirstOrDefault();
            }
        }

        public List<AudioAppointment> GetAudioAppointments(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return (from audioAppointment in dataContext.AudioAppointments
                        where audioAppointment.PatientId == patientID && audioAppointment.UniqueIdentifier == uniqueGuid
                        orderby audioAppointment.DateTime descending
                        select audioAppointment).ToList();
            }
        }

        public void DeleteAudioAppointment(int patientID, int appointmentId)
        {
            try
            {
                var appointment = GetAudioAppointment(patientID, appointmentId);
                if (appointment != null)
                {
                    using (var dataContext = new eHealthCareEntities())
                    {
                        dataContext.AudioAppointments.Attach(appointment);
                        dataContext.AudioAppointments.Remove(appointment);
                        dataContext.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                throw new Exception(ex.EntityValidationErrors.GetValidationErrors());
            }
            catch 
            {
                throw;
            }
        }

        #endregion
    }
}
