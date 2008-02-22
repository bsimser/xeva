using System;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public class ComposeRequestFilter : IChannelFilter
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
         object[] arguments = invocation.Arguments;

         channelRequest.Message.ServiceKey = channelRequest.ServiceName;
         channelRequest.Message.MethodKey = invocation.Method.Name;

         channelRequest.Message.MessageArgs = new MessageArgument[arguments.Length];

         for (int idx = 0; idx < arguments.Length; idx++)
         {
            Type argumentType = arguments[idx].GetType();

            MessageArgument arg = new MessageArgument();
            arg.ArgumentType = argumentType.AssemblyQualifiedName;
            arg.Argument = arguments[idx];
            channelRequest.Message.MessageArgs[idx] = arg;
         }
      }

   }
}