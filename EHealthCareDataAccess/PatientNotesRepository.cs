using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class PatientNotesRepository
    {
        Guid uniqueGuid;
        public PatientNotesRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public PatientNote GetPatientNoteById(int patientID, int patientNoteId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.PatientNotes.Where(i => i.Id == patientNoteId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public List<PatientNote> GetAllNotes(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.PatientNotes.Where(i => i.PatientId == patientID && i.UniqueIdentifier == uniqueGuid).ToList();
            }
        }

        public void SavePatientNotes(PatientNote patientNotes)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dataContext.PatientNotes.Add(patientNotes);
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

        public void DeletePatientNotes(int patientID, int patientNoteId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var patientNoteToDelete = GetPatientNoteById(patientID, patientNoteId);
                    dataContext.PatientNotes.Attach(patientNoteToDelete);
                    dataContext.PatientNotes.Remove(patientNoteToDelete);
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

        public void UpdatePatientNotes(int patientNoteId, PatientNote patientNote)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var patientNoteToUpdate = GetPatientNoteById(patientNote.PatientId, patientNoteId);
                    if (patientNoteToUpdate != null)
                    {
                        patientNoteToUpdate.Notes = patientNote.Notes;
                        patientNoteToUpdate.Date = patientNote.Date;
                        patientNoteToUpdate.Subject = patientNote.Subject;
                        dataContext.PatientNotes.Attach(patientNoteToUpdate);
                        dataContext.Entry(patientNoteToUpdate).State = EntityState.Modified;
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
