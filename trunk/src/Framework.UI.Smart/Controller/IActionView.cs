using System.Collections.Generic;
using System.Reflection;

namespace XF.UI.Smart
{
   public interface IActionView<TUpdateMessage>
   {
      void Attach(IActionCallbacks callback);
      TUpdateMessage RetrieveActionMessage();
      void AddControl(PropertyInfo output, object defaultValue, List<IListMessage> lookupList);
      void Show();
   }
}