using System.Collections.Generic;
using System.Reflection;

namespace XF.UI.Smart
{
   public interface IActionView<TUpdateMessage>
   {
      void Attach(IActionCallbacks callback);
      TUpdateMessage RetrieveActionMessage();
      IControl AddControl(string propertyName, string controlName, object controlValue, 
                          string controlLabel, EditableControl controlType, List<IListMessage> lookupList);
      void Show();
      void Close();
   }
}