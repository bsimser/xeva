using System;
using System.Collections.Generic;
using System.Text;
using XEVA.Framework.UI.Smart;

namespace BankTeller.UI.Smart
{
   public class WindowAdapter : IWindowAdapter
   {
      public IWindow NewWindow()
      {
         return new Window();
      }
   }
}
