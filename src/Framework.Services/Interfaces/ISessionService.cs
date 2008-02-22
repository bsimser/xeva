using System;

namespace XF.Services
{
   public interface ISessionService
   {
      Guid GenerateSessionTicket(Guid userID);
      bool ValidateSessionTicket(Guid ticketID);
      IUserAccount GetUserAccount(Guid ticketID);
      Guid Authenticate(object[] args);
   }
}