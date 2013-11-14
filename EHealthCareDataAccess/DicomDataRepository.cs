using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class DicomDataRepository
    {
        Guid uniqueGuid;

        public DicomDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public List<Dicom> GetAllDicom(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Dicoms.Where(i => i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).ToList();
            }
        }

        public Dicom GetDicomById(int patientID, int Id)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Dicoms.Where(i => i.Id == Id && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public void DeleteDicom(int patientID, int Id)
        {
            try
            {
                var dicom = GetDicomById(patientID, Id);
                if (dicom != null)
                {
                    using (var dataContext = new eHealthCareEntities())
                    {
                        dataContext.Dicoms.Attach(dicom);
                        dataContext.Dicoms.Remove(dicom);
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

        public void SaveDicom(Dicom dicom)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dicom.UniqueIdentifier = uniqueGuid;
                    dataContext.Dicoms.Add(dicom);
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
    }
}
