using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class WeightDataRepository
    {
        Guid uniqueGuid;
        public WeightDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public Weight GetWeightById(int patientID, int weightId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Weights.Where(i => i.Id == weightId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public List<Weight> GetAllWeightData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return (from w in dataContext.Weights
                        where w.PatientId == patientID && w.UniqueIdentifier == uniqueGuid
                        orderby w.Date descending
                        select w).ToList();
            }
        }

        public void SaveWeight(Weight weight)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dataContext.Weights.Add(weight);
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

        public void UpdateWeight(int weightId, Weight weight)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var weightToUpdate = GetWeightById(weight.PatientId, weightId);
                    if (weightToUpdate != null)
                    {
                        weightToUpdate.Date = weight.Date;
                        weightToUpdate.Weight1 = weight.Weight1;
                        weightToUpdate.WeightGoal = weight.WeightGoal;
                        dataContext.Weights.Attach(weightToUpdate);
                        dataContext.Entry(weightToUpdate).State = EntityState.Modified;
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
