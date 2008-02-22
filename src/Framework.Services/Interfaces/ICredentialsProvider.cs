using System;

namespace XF.Services
{
   public interface ICredentialsProvider
   {
      bool IsAuthenticating { get; set; }
      Guid SessionTicket { get; set; }
      Guid Authenticate(object[] args);
   }
}