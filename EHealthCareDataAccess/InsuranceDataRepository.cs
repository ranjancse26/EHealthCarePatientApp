using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class InsuranceDataRepository
    {
        Guid uniqueGuid;
        public InsuranceDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public List<Insurance> GetAllInsuranceData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Insurances.Where(i => i.PatientId == patientID && i.UniqueIdentifier == uniqueGuid).OrderBy(e => e.ExpirationDate).ToList();
            }
        }

        public Insurance GetInsuranceById(int patientID, int insuranceId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Insurances.Where(i => i.Id == insuranceId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public void SaveInsurance(Insurance insurance)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dataContext.Insurances.Add(insurance);
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

        public void UpdateInsurance(int insuranceId, Insurance insurance)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var insuranceToUpdate = GetInsuranceById(insurance.PatientId, insuranceId);
                    if (insuranceToUpdate != null)
                    {
                        insuranceToUpdate.PlanName = insurance.PlanName;
                        insuranceToUpdate.CoverageType = insurance.CoverageType;
                        insuranceToUpdate.IsPrimary = insurance.IsPrimary;
                        insuranceToUpdate.GroupNumber = insurance.GroupNumber;
                        insuranceToUpdate.SubscriberID = insurance.SubscriberID;
                        insuranceToUpdate.SubscriberDOB = insurance.SubscriberDOB;
                        insuranceToUpdate.SubscriberDate = insurance.SubscriberDate;
                        insuranceToUpdate.ExpirationDate = insurance.ExpirationDate;
                        dataContext.Insurances.Attach(insuranceToUpdate);
                        dataContext.Entry(insuranceToUpdate).State = EntityState.Modified;
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
