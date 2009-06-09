using System;

namespace XF.UI.Smart
{
   public class NoWindowControls : IWindowController
   {
      public void Show()
      {
      }

      public void ShowModal()
      {
         
      }

      public void ShowModal(int width, int height)
      {
         
      }

      public void Hide()
      {
      }

      public void Close()
      {
         
      }

      public event EventHandler Closed;

      public void ChangeCaption(string caption)
      {
      }
   }
}