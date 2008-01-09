using System;
using System.ComponentModel;

namespace XEVA.Framework.UI.Smart
{
   public interface IWindow : IWindowController
   {
      void Close();

      event EventHandler Closed;

      void InitializeUI(object ui);
   }
}