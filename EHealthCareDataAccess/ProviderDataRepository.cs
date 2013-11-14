using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public class ProviderDataRepository
    {
        /// <summary>
        /// Save Provider Record
        /// </summary>
        /// <param name="patient"></param>
        public void SaveProviderRecord(Provider provider)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                try
                {
                    var patientFound = dataContext.Providers.FirstOrDefault(p => p.UserName == provider.UserName);
                    if (patientFound != null)
                        throw new Exception("User already exists!");

                    dataContext.Providers.Add(provider);
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

        /// <summary>
        /// Get all providers.
        /// </summary>
        /// <returns>List<Provider></returns>
        public List<Provider> GetAllProviders()
        {
            using (var dataContext = new eHealthCareEntities())
            {
                return dataContext.Providers.ToList();
            }
        }

        /// <summary>
        /// Get Provider By Id
        /// </summary>
        /// <param name="username">UserName</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public string GetProviderById(int providerId)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                var provider = dataContext.Providers.FirstOrDefault(p => p.Id == providerId);
                return provider.FirstName + " " + provider.LastName;
            }
        }

        /// <summary>
        /// Get Provider Username, Password
        /// </summary>
        /// <param name="username">UserName</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public Provider GetProviderByUserNamePassword(string username, string password)
        {
            using (var dataContext = new eHealthCareEntities())
            {
                var provider = dataContext.Providers.FirstOrDefault(p => p.UserName == username && p.Password == password);
                return provider;
            }
        }
    }
}
