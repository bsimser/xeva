using System;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public class ComposeRequestFilter : IChannelFilter
   {
      private RequestState _requestState;
      private ResponseState _responseState;

      public RequestState RequestState
      {
         get { return _requestState; }
         set { _requestState = value; }
      }

      public ResponseState ResponseState
      {
         get { return _responseState; }
         set { _responseState = value; }
      }

      public void Process()
      {
         IInvocation invocation = _requestState.Invocation;
         object[] arguments = invocation.Arguments;

         _requestState.Message.ServiceKey = _requestState.ServiceName;
         _requestState.Message.MethodKey = invocation.Method.Name;

         _requestState.Message.MessageArgs = new MessageArgument[arguments.Length];

         for (int idx = 0; idx < arguments.Length; idx++)
         {
            Type argumentType = arguments[idx].GetType();

            MessageArgument arg = new MessageArgument();
            arg.ArgumentType = argumentType.AssemblyQualifiedName;
            arg.Argument = arguments[idx];
            _requestState.Message.MessageArgs[idx] = arg;
         }
      }

   }
}