using System;

namespace XF.Services
{
   public class CredentialsProvider : ICredentialsProvider
   {
      private Guid _sessionTicket;

      public Guid SessionTicket
      {
         get { return _sessionTicket; }
         set { _sessionTicket = value; }
      }
   }
}