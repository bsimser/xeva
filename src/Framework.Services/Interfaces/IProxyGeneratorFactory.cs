using System;
using Castle.Core.Interceptor;

namespace XF.Services
{
   public interface IProxyGeneratorFactory
   {
      object CreateInterfaceProxyWithoutTarget(Type serviceType, IInterceptor interceptor);
   }
}