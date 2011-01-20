using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XF.UI.Smart;

namespace XF.Controls {
   public class XFSmartControlFactory {
      public static IEditable BuildEditableControl(string controlName, string controlLabel, object controlValue,
                                                   EditableControl controlType, List<IListMessage> lookupList,
                                                   bool isReadOnly) {
         var type = controlType != EditableControl.EditableTextbox ? controlType.ToString() : typeof(EditableTextbox).Name;
         var control = Activator.CreateInstance(Type.GetType("XF.Controls." + type), null) as IEditable;
         control.Name = controlName;
         control.ReadOnly = isReadOnly;
         return control.ToIEditable(controlLabel, controlValue, lookupList);
      }
   }
}