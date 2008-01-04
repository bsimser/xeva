using System.Collections.Generic;

namespace XEVA.Framework.Validation
{
   public interface IValidator
   {
      Notification Validate(object target, Dictionary<string, IValidationObject> validationObjects);
   }
}