using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class PatientDataRepository
    {
        /// <summary>
        /// Save Patient Record
        /// </summary>
        /// <param name="patient"></param>
        public void SavePatientRecord(Patient  patient)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                var patientFound = dataContext.Patients.FirstOrDefault(p => p.UserName == patient.UserName);
                if (patientFound != null)
                    throw new Exception("User already exists!");

                dataContext.Patients.Add(patient);
                dataContext.SaveChanges();
            }
        }

        public List<Patient> GetAllPatients()
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Patients.ToList();
            }
        }

        /// <summary>
        /// Get Patient Username, Password
        /// </summary>
        /// <param name="username">UserName</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public Patient GetPatientByUserNamePassword(string username, string password)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                var patient = dataContext.Patients.FirstOrDefault(p => p.UserName == username && p.Password == password);
                return patient;
            }
        }


        public Guid GetPatientUniqueIdentifier(int patientId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                var patient = dataContext.Patients.FirstOrDefault(a => a.PatientId == patientId);
                return patient == null ? Guid.Empty : patient.UniqueIdentifier;
            }
        }
    }
}
