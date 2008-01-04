using System;
using System.Collections.Generic;
using System.Text;
using XEVA.Framework.Services;

namespace XEVA.Framework.Messages
{
   public class RequestMessage<T> : Message
      where T : class, new()
   {
      private T _payload;

      private int _timeout;

      private Guid _sessionTicket;

      public T Payload
      {
         get
         {
            return _payload;
         }
         set
         {
            _payload = value;
         }
      }

      public int Timeout
      {
         get { return this._timeout; }
         set { this._timeout = value; }
      }

      public Guid SessionTicket
      {
         get { return this._sessionTicket; }
         set { this._sessionTicket = value; }
      }
   }
}
