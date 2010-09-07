using System.Collections.Generic;
using System.Drawing;
using XF;
using XF.UI.Smart;
using XF.Validation;

namespace XF.UI.Smart
{
   public interface IEditable : IControl
   {
      void SetToEdit(bool isInEdit);
      void EnableEdit(bool isInEdit);
      void ResetValue();
      object EditedValue { get; }
      IEditable ToIEditable(string controlLabel, object controlValue, List<IListMessage> lookupList);
      Color ControlBackcolor { set; }
   }
}