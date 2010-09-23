using System.Diagnostics;
using Infragistics.Win;
using Infragistics.Win.Misc;

namespace XF.Controls {
   internal class DropdownButtonDrawFilter : IUIElementDrawFilter {
      public DrawPhase GetPhasesToFilter(ref UIElementDrawParams drawParams) {
         if (drawParams.Element is SplitButtonUIElement ||
            drawParams.Element is SplitButtonDropDownUIElement) {
            return DrawPhase.AfterDrawTheme;
         }

         return DrawPhase.None;
      }

      public bool DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams) {
         // draw after the element theme as been drawn
         if (DrawPhase.AfterDrawTheme == drawPhase) {
            // get the button element
            var buttonElement = drawParams.Element as ButtonUIElementBase;

            if (buttonElement != null) {
               // get the button control and current button state
               var dropDown = drawParams.ControlElement.Control as UltraDropDownButton;
               //var buttonState = buttonElement.ButtonState;
               var buttonState = UIElementButtonState.MouseOver;

               if (!dropDown.IsDroppedDown && !buttonElement.IsMouseOver) {

                     XPThemes.ToolBar.DrawToolbarButton(
                        false, drawParams.Element is SplitButtonDropDownUIElement,
                        buttonState, ref drawParams,
                        drawParams.Element.Rect, drawParams.InvalidRect);
               }
            }
         }

         return false;
      }
   }
}