using System;
using System.Collections.Generic;
using System.Text;

namespace BankTeller.UI.Smart.Services
{
   public interface IAuthenticationService
   {
      bool Authenticate(string username, string password);
   }
}
