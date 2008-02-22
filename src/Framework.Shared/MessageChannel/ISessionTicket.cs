using System;

namespace XF
{
   public interface ISessionTicket
   {
      IUserAccount UserAccount { get; }
      DateTime Expiration { get; }
      bool IsValid();
   }
}