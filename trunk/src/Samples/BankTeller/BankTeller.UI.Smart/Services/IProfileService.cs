using System;
using System.Collections.Generic;
using System.Text;

namespace BankTeller.UI.Smart.Services
{
   public interface IProfileService
   {
      IDictionary<string, string> GetShellTools();
   }
}
