using System.Collections.Generic;

namespace XEVA.Framework.UI.Smart
{
   public interface IValidationService
   {
      bool Validate(object target, Dictionary<string, IControl> controls);
   }
}
