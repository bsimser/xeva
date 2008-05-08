using System;
using System.Reflection;

namespace XF.Services
{
   public class CredentialsProvider<TAuthenticationService> : ICredentialsProvider 
      where TAuthenticationService : IAuthenticationService 
   {
      private Guid _sessionTicket;
      private bool _isAuthenticating;

      public bool IsAuthenticating
      {
         get { return _isAuthenticating; }
         set { _isAuthenticating = value; }
      }

      public Guid SessionTicket
      {
         get { return _sessionTicket; }
         set { _sessionTicket = value; }
      }

      public Guid Authenticate(object[] args)
      {
         _isAuthenticating = true;
         TAuthenticationService authenticationService = Locator.Resolve<TAuthenticationService>();

         return authenticationService.Authenticate(args);
      }
   }
}