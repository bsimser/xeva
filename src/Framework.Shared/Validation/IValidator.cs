using System.Collections.Generic;

namespace XF.Validation
{
   public interface IValidator
   {
      ValidationResult Validate(object target, Dictionary<string, IValidationAware> validationObjects);
   }
}