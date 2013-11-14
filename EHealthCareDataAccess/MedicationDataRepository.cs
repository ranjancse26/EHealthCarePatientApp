using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class MedicationDataRepository
    {
        Guid uniqueGuid;
        StringBuilder validationErrors;

        public MedicationDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
            validationErrors = new StringBuilder();
        }

        public List<MedicationStregth> GetAllMedicationStrengths()
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.MedicationStregths.ToList();
            }
        }

        public List<MedicationDose> GetAllMedicationDoses()
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.MedicationDoses.ToList();
            }
        }

        public List<MedicationTaken> GetAllMedicationTakens()
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.MedicationTakens.ToList();
            }
        }

        public List<Medication> GetAllMedicationData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Medications.Where(i => i.PatientId == patientID).OrderBy(e => e.StartDate).ToList();
            }
        }

        public void SaveMedication(Medication medication)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    medication.UniqueIdentifier = this.uniqueGuid;
                    dataContext.Medications.Add(medication);
                    dataContext.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.EntityValidationErrors.GetValidationErrors());
                }
            }  
        }

        public Medication GetMedicationById(int patientID, int medicationId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Medications.Where(i => i.Id == medicationId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public void DeleteMedication(int patientID, int medId)
        {
            try
            {
                var medication = GetMedicationById(patientID, medId);
                if (medication != null)
                {
                    using (var dataContext = new eHealthCareEntities())
                    {
                        dataContext.Medications.Attach(medication);
                        dataContext.Medications.Remove(medication);
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

        public void UpdateMedication(int medicationId, Medication medication)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var medicationToUpdate = GetMedicationById(medication.PatientId, medicationId);
                    if (medicationToUpdate != null)
                    {
                        medicationToUpdate.DoseText = medication.DoseText;
                        medicationToUpdate.DoseUnit = medication.DoseUnit;
                        medicationToUpdate.HowTaken = medication.HowTaken;
                        medicationToUpdate.MedicationName = medication.MedicationName;
                        medicationToUpdate.Notes = medication.Notes;
                        medicationToUpdate.ReasonForTaking = medication.ReasonForTaking;
                        medicationToUpdate.StrengthText = medication.StrengthText;
                        medicationToUpdate.StrengthUnit = medication.StrengthUnit;
                        if (medication.EndDate != null)
                            medicationToUpdate.EndDate = medication.EndDate;
                        dataContext.Medications.Attach(medicationToUpdate);
                        dataContext.Entry(medicationToUpdate).State = EntityState.Modified;
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
