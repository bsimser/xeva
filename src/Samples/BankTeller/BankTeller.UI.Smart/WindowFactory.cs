using System;
using System.Collections.Generic;
using System.Text;
using XF.UI.Smart;

namespace BankTeller.UI.Smart
{
   public class WindowFactory 
   {
      public virtual IWindowAdapter Create()
      {
         return new WindowAdapter();
      }
   }
}
