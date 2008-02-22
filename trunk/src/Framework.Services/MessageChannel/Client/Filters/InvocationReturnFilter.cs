using Castle.Core.Interceptor;

namespace XF.Services
{
   public class InvocationReturnFilter : IChannelFilter
   {
      private ChannelRequest channelRequest;
      private ChannelResponse channelResponse;

      public ChannelRequest ChannelRequest
      {
         get { return channelRequest; }
         set { channelRequest = value; }
      }

      public ChannelResponse ChannelResponse
      {
         get { return channelResponse; }
         set { channelResponse = value; }
      }

      public void Process()
      {
         IInvocation invocation = channelRequest.Invocation;
         ExceptionMessage exceptionMessage = channelResponse.Message.ExceptionMessage;

         if(exceptionMessage != null)
         {
            if (exceptionMessage.ErrorLevel == "Warning" &&
                channelResponse.Message.ResponseObject != null)
               invocation.ReturnValue = channelResponse.Message.ResponseObject;
         }
         else
         {
            if (channelResponse.Message.ResponseObject != null)
            invocation.ReturnValue = channelResponse.Message.ResponseObject;
         }
      }

   }
}