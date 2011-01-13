using Infragistics.Win;
using Infragistics.Win.UltraWinTabControl;

namespace XF.Controls {
   public class TabControlDrawFilter : IUIElementDrawFilter {
      public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams) {
         if (drawParams.Element is TabPageAreaUIElement) 
            return DrawPhase.BeforeDrawBorders;

         return DrawPhase.None;
      }

      public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams) {
         return drawPhase == DrawPhase.BeforeDrawBorders;
      }
   }
}