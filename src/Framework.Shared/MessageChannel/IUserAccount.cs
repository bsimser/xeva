using System;

namespace XF
{
   public interface IUserAccount
   {
      Guid ID { get; }
      string Username { get; }
   }
}