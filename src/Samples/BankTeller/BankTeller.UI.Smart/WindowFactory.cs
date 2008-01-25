using System;
using System.Collections.Generic;
using System.Text;
using XF.UI.Smart;

namespace BankTeller.UI.Smart
{
   public class WindowFactory : IWindowFactory
   {
      public IWindowAdapter Create()
      {
         return new WindowAdapter();
      }
   }
}
