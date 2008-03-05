using System.Collections.Generic;

namespace XF.Validation
{
   public interface IValidator
   {
      ValidationResult Validate(object[] targets, Dictionary<string, IValidationAware> validationObjects);
   }
}