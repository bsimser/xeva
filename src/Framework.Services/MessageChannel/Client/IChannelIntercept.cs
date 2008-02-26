using System;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public interface IChannelIntercept : IInterceptor
   {
      string ServiceName { get; set; }
      Type ServiceType { get; set; }
   }
}