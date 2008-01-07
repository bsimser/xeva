using System;
using System.ComponentModel;

namespace XEVA.Framework.UI.Smart
{
   public interface IWindowView<TCallbacks> : IView<TCallbacks>
      where TCallbacks : IPresenterCallbacks

   {
      void Show();

      void Hide();

      void Close();

      event CancelEventHandler Closing;

      event EventHandler Closed;
   }
}