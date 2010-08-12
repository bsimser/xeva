using System.Collections.Generic;
using XF.Controls;

namespace XF.UI.Smart
{
   public interface IActionView<TUpdateMessage>
   {
      string Title { set; }
      void Attach(IActionCallbacks callback);
      TUpdateMessage RetrieveActionMessage();
      IControl AddControl(string propertyName, string controlName, object controlValue, 
                          string controlLabel, EditableControl controlType, List<IListMessage> lookupList);
      void Show();
      void Close();
   }
}