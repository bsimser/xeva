using System.Collections.Generic;

namespace XF.UI.Smart
{
   public interface IPresenterValidator
   {
      bool Validate(object[] targets, Dictionary<string, IControl> controls);
   }
}
