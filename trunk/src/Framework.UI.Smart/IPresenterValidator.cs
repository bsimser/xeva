using System.Collections.Generic;

namespace XEVA.Framework.UI.Smart
{
   public interface IPresenterValidator
   {
      bool Validate(object target, Dictionary<string, IControl> controls);
   }
}
