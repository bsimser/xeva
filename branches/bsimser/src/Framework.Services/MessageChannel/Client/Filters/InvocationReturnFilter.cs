using Castle.Core.Interceptor;

namespace XF.Services
{
   public class InvocationReturnFilter : IChannelFilter
   {
      private ChannelRequest _channelRequest;
      private ChannelResponse _channelResponse;

      public ChannelRequest ChannelRequest
      {
         get { return _channelRequest; }
         set { _channelRequest = value; }
      }

      public ChannelResponse ChannelResponse
      {
         get { return _channelResponse; }
         set { _channelResponse = value; }
      }

      public void Process()
      {
         if (_channelResponse.Message.ResponseObject != null)
            _channelRequest.Invocation.ReturnValue = _channelResponse.Message.ResponseObject;
      }

   }
}