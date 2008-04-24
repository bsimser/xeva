using System;

namespace XF.Services
{
   public interface IAuthenticationService
   {
      Guid Authenticate(object[] args);
   }
}