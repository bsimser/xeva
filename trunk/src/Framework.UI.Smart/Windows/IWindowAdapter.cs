using System;
using System.ComponentModel;

namespace XF.UI.Smart
{
   public interface IWindowAdapter : IWindowController, IWindowOptions
   {
      void Close();

      event EventHandler Closed;

      void InitializeUI(object ui);
   }
}