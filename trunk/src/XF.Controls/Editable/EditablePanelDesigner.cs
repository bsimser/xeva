using System.Windows.Forms.Design;

namespace XF.Controls {
   public class EditablePanelDesigner : ControlDesigner {
      public override void Initialize(System.ComponentModel.IComponent component) {
         base.Initialize(component);
         var uc = (EditablePanel)component;

         EnableDesignMode(uc.Panel1, "Panel1");
      }
   }
}