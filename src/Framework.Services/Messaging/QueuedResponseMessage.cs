using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.Messages
{
   public class QueuedResponseMessage : ResponseMessage
   {
      private Guid _eventID;

      public Guid EventID
      {
         get { return this._eventID; }
         set { this._eventID = value; }
      }
   }
}
