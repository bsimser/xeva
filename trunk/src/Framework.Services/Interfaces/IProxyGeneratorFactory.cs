using System;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public interface IProxyGeneratorFactory
   {
      object GenerateProxy(Type serviceType, IInterceptor interceptor);
   }
}