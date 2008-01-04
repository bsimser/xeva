using System;
using System.Collections.Generic;
using System.Text;

namespace XEVA.Framework.Messages
{
   public class Message
   {
      private string _service;
      private string _operation;

      public string Service
      {
         get { return this._service; }
         set { this._service = value; }
      }

      public string Operation
      {
         get { return this._operation; }
         set { this._operation = value; }
      }
   }
}
