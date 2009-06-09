using Castle.Core.Interceptor;

namespace XF.Services
{
   public class ChannelRequest
   {
      private string _serviceName;
      private RequestMessage _message;
      private IInvocation _invocation;
      private byte[] _content;

      public string ServiceName
      {
         get { return _serviceName; }
         set { _serviceName = value; }
      }

      public byte[] Content
      {
         get { return _content; }
         set { _content = value; }
      }

      public RequestMessage Message
      {
         get { return _message; }
         set { _message = value; }
      }

      public IInvocation Invocation
      {
         get { return _invocation; }
         set { _invocation = value; }
      }
   }
}