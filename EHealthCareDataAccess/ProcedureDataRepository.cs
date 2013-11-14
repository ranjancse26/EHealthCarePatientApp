using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class ProcedureDataRepository
    {
        Guid uniqueGuid;
        public ProcedureDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public Procedure GetProcedureById(int patientID, int procedureId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Procedures.Where(i => i.Id == procedureId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public List<Procedure> GetAllProcedureData(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Procedures.Where(i => i.PatientId == patientID && 
                    i.UniqueIdentifier == uniqueGuid).OrderBy(e => e.Date).ToList();
            }
        }

        public void SaveProcedure(Procedure procedure)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dataContext.Procedures.Add(procedure);
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

        public void UpdateProcedure(int procedureId, Procedure procedure)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var procedureToUpdate = GetProcedureById(procedure.PatientId, procedureId);
                    if (procedureToUpdate != null)
                    {
                        procedureToUpdate.Date = procedure.Date;
                        procedureToUpdate.Notes = procedure.Notes;
                        procedureToUpdate.PrimaryProviderId = procedure.PrimaryProviderId;
                        procedureToUpdate.ProcedureName = procedure.ProcedureName;
                        procedureToUpdate.SecondaryProviderId = procedure.SecondaryProviderId;
                        dataContext.Procedures.Attach(procedureToUpdate);
                        dataContext.Entry(procedureToUpdate).State = EntityState.Modified;
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
