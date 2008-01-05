using System.Collections.Generic;

namespace XEVA.Framework.Validation
{
   public interface IValidator
   {
      ValidationResult Validate(object target, Dictionary<string, IValidationObject> validationObjects);
   }
}