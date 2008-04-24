using System;

namespace XF.Services
{
   public class ServiceResponse
   {
      private ResponseMessage _message = new ResponseMessage();
      private byte[] _content;
      private Exception _serviceException;

      public ResponseMessage Message
      {
         get { return _message; }
         set { _message = value; }
      }

      public byte[] Content
      {
         get { return _content; }
         set { _content = value; }
      }

      public Exception ServiceException
      {
         get { return _serviceException; }
         set { _serviceException = value; }
      }
   }
}