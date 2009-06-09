using System;

namespace XF.UI.Smart
{
   public interface IWindowController
   {
      void Show();
      void Hide();
      void Close();
      event EventHandler Closed;
      void ChangeCaption(string caption);
   }
}