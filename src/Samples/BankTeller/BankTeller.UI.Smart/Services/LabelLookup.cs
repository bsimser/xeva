using System;
using System.Collections.Generic;
using System.Text;

namespace BankTeller.UI.Smart.Services
{
   public class LabelLookup : ILabelLookup
   {
      public string Find(string code)
      {
         return code;
      }
   }
}
