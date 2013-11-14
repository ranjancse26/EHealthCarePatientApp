using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class ContactsDataRepository
    {
        Guid uniqueGuid;
        public ContactsDataRepository(Guid uniqueGuid)
        {
            this.uniqueGuid = uniqueGuid;
        }

        public Contact GetContactById(int patientID, int contactId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Contacts.Where(i => i.Id == contactId && i.PatientId == patientID &&
                    i.UniqueIdentifier == uniqueGuid).FirstOrDefault();
            }
        }

        public List<Contact> GetAllContacts(int patientID)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Contacts.Where(i => i.PatientId == patientID && i.UniqueIdentifier == uniqueGuid).ToList();
            }
        }

        public void SaveContact(Contact contact)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    dataContext.Contacts.Add(contact);
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

        public void UpdateContact(int contactId, Contact contact)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var contactToUpdate = GetContactById(contact.PatientId, contactId);
                    if (contactToUpdate != null)
                    {
                        contactToUpdate.City = contact.City;
                        contactToUpdate.Country = contact.Country;
                        contactToUpdate.EmailAddress = contact.EmailAddress;
                        contactToUpdate.IsPrimary = contact.IsPrimary;
                        contactToUpdate.Phone = contact.Phone;
                        contactToUpdate.PhoneType = contact.PhoneType;
                        contactToUpdate.StateOrProvince = contact.StateOrProvince;
                        contactToUpdate.StreetAddress1 = contact.StreetAddress1;
                        contactToUpdate.StreetAddress2 = contact.StreetAddress2;
                        contactToUpdate.StreetAddress3 = contact.StreetAddress3;
                        dataContext.Contacts.Attach(contactToUpdate);
                        dataContext.Entry(contactToUpdate).State = EntityState.Modified;
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
