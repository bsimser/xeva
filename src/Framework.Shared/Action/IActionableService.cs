using System;
using System.Collections.Generic;

namespace XF
{
   public interface IActionableService
   {
      IUserAccount UserAccount { get; set; }

      void LogAction(IUserAccount performedBy, DateTime performedAt, Type actionType ,
                     List<IActionParameters> parameters);
   }
}