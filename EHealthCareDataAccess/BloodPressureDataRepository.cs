using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class BloodPressureDataRepository
    {
        Guid uniqueGuid;
        public BloodPressureDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public BloodPressure GetBloodPressureById(int patientID, int bloodPressureId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.BloodPressures.Where(i => i.Id == bloodPressureId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public List<BloodPressure> GetAllBloodPressureData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.BloodPressures.Where(i => i.PatientId == patientID && i.UniqueIdentifier == uniqueGuid).OrderBy(e => e.When).ToList();
            }
        }

        public void SaveBloodPressure(BloodPressure bloodPressure)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    bloodPressure.UniqueIdentifier = this.uniqueGuid;
                    dataContext.BloodPressures.Add(bloodPressure);
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

        public void UpdateBloodPressure(int patientNoteId, BloodPressure bloodPressure)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var bloodPressureToUpdate = GetBloodPressureById(bloodPressure.PatientId, patientNoteId);
                    if (bloodPressureToUpdate != null)
                    {
                        bloodPressureToUpdate.Systolic = bloodPressure.Systolic;
                        bloodPressureToUpdate.Diastolic = bloodPressure.Diastolic;
                        bloodPressureToUpdate.IrregularHeartbeat = bloodPressure.IrregularHeartbeat;
                        bloodPressureToUpdate.Pulse = bloodPressure.Pulse;
                        bloodPressureToUpdate.When = bloodPressure.When;
                        dataContext.BloodPressures.Attach(bloodPressureToUpdate);
                        dataContext.Entry(bloodPressureToUpdate).State = EntityState.Modified;
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
    }
}
