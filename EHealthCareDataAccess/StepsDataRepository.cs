using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class StepsDataRepository
    {
        Guid uniqueGuid;
        public StepsDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public Step GetStepById(int patientID, int stepId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Steps.Where(i => i.Id == stepId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public List<Step> GetAllStepsData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Steps.Where(i => i.PatientId == patientID && i.UniqueIdentifier == uniqueGuid).OrderBy(e => e.Date).ToList();
            }
        }

        public void SaveSteps(Step step)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dataContext.Steps.Add(step);
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

        public void UpdateStep(int stepId, Step step)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var stepToUpdate = GetStepById(step.PatientId, stepId);
                    if (stepToUpdate != null)
                    {
                        stepToUpdate.Date = step.Date;
                        stepToUpdate.Steps = step.Steps;
                        dataContext.Steps.Attach(stepToUpdate);
                        dataContext.Entry(stepToUpdate).State = EntityState.Modified;
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
