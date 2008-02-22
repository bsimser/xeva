using System;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public interface IChannelIntercept : IInterceptor
   {
      event EventHandler TransportFailed;
      ITransport Transport { get; }
      string ServiceName { get; set; }
      Type ServiceType { get; set; }
   }
}