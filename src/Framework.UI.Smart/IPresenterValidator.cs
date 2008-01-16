using System.Collections.Generic;

namespace XF.UI.Smart
{
   public interface IPresenterValidator
   {
      bool Validate(object target, Dictionary<string, IControl> controls);
   }
}
