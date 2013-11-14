using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class ConditionDataRepository
    {
        Guid uniqueGuid;
        public ConditionDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public List<Condition> GetAllConitionData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Conditions.Where(i => i.PatientId == patientID).OrderBy(e => e.StartDate).ToList();
            }
        }

        public Condition GetConditionById(int patientID, int conditionId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Conditions.Where(i => i.Id == conditionId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public void SaveCondition(Condition condition)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dataContext.Conditions.Add(condition);
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

        public void UpdateCondition(int patientNoteId, Condition condition)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var conditionToUpdate = GetConditionById(condition.PatientId, patientNoteId);
                    if (conditionToUpdate != null)
                    {
                        conditionToUpdate.Name = condition.Name;
                        conditionToUpdate.Notes = condition.Notes;
                        conditionToUpdate.Recovered = condition.Recovered;
                        conditionToUpdate.StartDate = condition.StartDate;
                        conditionToUpdate.EndDate = condition.EndDate;
                        conditionToUpdate.Status = condition.Status;                 
                        dataContext.Conditions.Attach(conditionToUpdate);
                        dataContext.Entry(conditionToUpdate).State = EntityState.Modified;
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
