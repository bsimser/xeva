using System.Collections.Generic;

namespace BankTeller.UI.Smart.Services
{
   public class ProfileService : IProfileService
   {
   
      public IDictionary<string, string> GetShellTools()
      {
         return new Dictionary<string, string>
                   {
                      {"Tools.Search", "Search"},
                      {"Tools.Tasks", "Tasks"},
                      {"Tools.Admin", "Admin"}
                   };
      }

   }
}