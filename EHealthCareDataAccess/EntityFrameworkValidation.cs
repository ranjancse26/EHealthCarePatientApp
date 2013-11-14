using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHealthCareDataAccess
{
    public static class EntityFrameworkValidation
    {
        public static string GetValidationErrors(this IEnumerable<DbEntityValidationResult> validationResults)
        {
            StringBuilder validationErrors = new StringBuilder();
            foreach (var validationResult in validationResults)
            {
                if (validationResult.IsValid == false)
                {
                    foreach (var error in validationResult.ValidationErrors)
                    {
                        validationErrors.AppendLine(error.ErrorMessage);
                    }
                }
            }
            return validationErrors.ToString();
        }
    }
}
