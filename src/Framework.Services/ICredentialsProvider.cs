using System;

namespace XF.Services
{
   public interface ICredentialsProvider
   {
      Guid SessionTicket { get; set; }
   }
}