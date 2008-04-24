using System;

namespace XF.Services
{
   public class FakeAuthentication : IAuthenticationService
   {
      public Guid Authenticate(object[] args)
      {
         return Guid.NewGuid();
      }
   }
}