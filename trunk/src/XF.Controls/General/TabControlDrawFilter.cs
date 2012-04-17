using Infragistics.Win;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTabs;

namespace XF.Controls {
   public class TabControlDrawFilter : IUIElementDrawFilter {
      public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams) {
         if (drawParams.Element is TabPageAreaUIElement)
            return DrawPhase.BeforeDrawBorders;

         if (drawParams.Element is TabItemUIElement)
            return DrawPhase.BeforeDrawFocus;

         return DrawPhase.None;
      }

      public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams) {
         return drawPhase == DrawPhase.BeforeDrawBorders || 
                drawPhase == DrawPhase.BeforeDrawFocus;
      }
   }

   public class TabControlNoFocusDrawFilter : IUIElementDrawFilter {
      public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams) {
         return true;
      }
      public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams) {

         return DrawPhase.None;
      }
   }
}
