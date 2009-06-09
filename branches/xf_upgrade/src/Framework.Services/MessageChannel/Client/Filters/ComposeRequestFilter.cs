using System;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public class ComposeRequestFilter : IChannelFilter
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
         RequestMessage request = new RequestMessage();

         IInvocation invocation = _channelRequest.Invocation;
         object[] arguments = invocation.Arguments;

         request.ServiceKey = _channelRequest.ServiceName;
         request.MethodKey = invocation.Method.Name;

         request.MessageArgs = new MessageArgument[arguments.Length];

         for (int idx = 0; idx < arguments.Length; idx++)
         {
            Type argumentType = arguments[idx].GetType();

            MessageArgument arg = new MessageArgument();
            arg.ArgumentType = argumentType.AssemblyQualifiedName;
            arg.Argument = arguments[idx];
            request.MessageArgs[idx] = arg;
         }

         _channelRequest.Message = request;
      }

   }
}