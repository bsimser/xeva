using System.Collections.Generic;
using XF.Controls;

namespace XF.UI.Smart
{
   public interface IPresenterValidator
   {
      bool Validate(object[] targets, Dictionary<string, IControl> controls);
   }
}
