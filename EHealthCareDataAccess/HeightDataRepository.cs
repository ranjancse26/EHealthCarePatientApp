using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class HeightDataRepository
    {
        Guid uniqueGuid;
        public HeightDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public Height GetHeightById(int patientID, int heightId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Heights.Where(i => i.Id == heightId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public List<Height> GetAllHeightData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Heights.Where(i => i.PatientId == patientID && 
                    i.UniqueIdentifier == uniqueGuid).OrderBy(e => e.Date).ToList();
            }
        }

        public void SaveHeight(Height height)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dataContext.Heights.Add(height);
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

        public void UpdateHeight(int heightId, Height height)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var heightToUpdate = GetHeightById(height.PatientId, heightId);
                    if (heightToUpdate != null)
                    {
                        heightToUpdate.Date = height.Date;
                        heightToUpdate.HeightFeet = height.HeightFeet;
                        heightToUpdate.HeightInch = height.HeightInch;
                        dataContext.Heights.Attach(heightToUpdate);
                        dataContext.Entry(heightToUpdate).State = EntityState.Modified;
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
