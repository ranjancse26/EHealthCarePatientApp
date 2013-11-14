using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class DietaryIntakesDataRepository
    {
        Guid uniqueGuid;
        public DietaryIntakesDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public DietaryIntake GetDietaryIntakesById(int patientID, int dietaryIntakesById)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.DietaryIntakes.Where(i => i.Id == dietaryIntakesById && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public List<DietaryIntake> GetAllDietaryIntakeData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.DietaryIntakes.Where(i => i.PatientId == patientID && 
                    i.UniqueIdentifier == uniqueGuid).OrderBy(e => e.Date).ToList();
            }
        }

        public void SaveDietaryIntake(DietaryIntake dietaryIntake)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dataContext.DietaryIntakes.Add(dietaryIntake);
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

        public void UpdateDietaryIntake(int dietaryIntakeId, DietaryIntake dietaryIntake)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var dietaryIntakeToUpdate = GetDietaryIntakesById(dietaryIntake.PatientId, dietaryIntakeId);
                    if (dietaryIntakeToUpdate != null)
                    {
                        dietaryIntakeToUpdate.Calories = dietaryIntake.Calories;
                        dietaryIntakeToUpdate.Date = dietaryIntake.Date;
                        dietaryIntakeToUpdate.Meal = dietaryIntake.Meal;
                        dietaryIntakeToUpdate.Name = dietaryIntake.Name;
                        dietaryIntakeToUpdate.Notes = dietaryIntake.Notes;
                        dataContext.DietaryIntakes.Attach(dietaryIntakeToUpdate);
                        dataContext.Entry(dietaryIntakeToUpdate).State = EntityState.Modified;
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
