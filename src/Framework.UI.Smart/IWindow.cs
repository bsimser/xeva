using System;
using System.ComponentModel;

namespace XEVA.Framework.UI.Smart
{
   public interface IWindow
   {
      void Show();

      void Hide();

      void Close();

      void InitializeUI(object ui);
   }
}