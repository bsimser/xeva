using System.Collections.Generic;
using XF.Validation;

namespace XF.UI.Smart
{
   public class PresenterValidator : IPresenterValidator
   {
      public virtual bool Validate(object[] targets, Dictionary<string, IControl> controls)
      {
         Dictionary<string, IValidationAware> validationObjects = new Dictionary<string, IValidationAware>();

         foreach (KeyValuePair<string, IControl> control in controls)
         {
            validationObjects.Add(control.Key, control.Value);
         }

         return new Validator().Validate(targets, validationObjects).IsValid;
      }
   }
}
