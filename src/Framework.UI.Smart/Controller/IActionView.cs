using System.Collections.Generic;

namespace XF.UI.Smart
{
   public interface IActionView<TUpdateMessage>
   {
      string Title { set; }
      void Attach(IActionCallbacks callback);
      TUpdateMessage RetrieveActionMessage();
      IControl AddControl(string propertyName, string controlName, object controlValue, 
                          string controlLabel, EditableControl controlType, List<IListMessage> lookupList,
                          bool isReadOnly);
      void Show();
      void Close();
      void ShowMessage(string message);
   }
}