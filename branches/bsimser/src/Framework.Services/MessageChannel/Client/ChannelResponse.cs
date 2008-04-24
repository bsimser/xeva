using Castle.Core.Interceptor;

namespace XF.Services
{
   public class ChannelResponse
   {
      private ResponseMessage _message = new ResponseMessage();
      private byte[] _content;

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
   }
}