using System.Collections.Generic;
using XF.Controls;
using XF.Validation;

namespace XF.UI.Smart
{
   public class ControllerValidator : IPresenterValidator
   {
      public virtual bool Validate(object[] targets, Dictionary<string, IControl> controls)
      {
         var validationObjects = new Dictionary<string, IValidationAware>();

         foreach (KeyValuePair<string, IControl> control in controls)
         {
            validationObjects.Add(control.Key, control.Value);
         }

         return new Validator().Validate(targets, validationObjects).IsValid;
      }
   }
}
