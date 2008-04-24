using System;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace XF.Services
{
   public class ProxyGeneratorFactory : IProxyGeneratorFactory
   {
      private ProxyGenerator _proxyGenerator;

      public ProxyGeneratorFactory(ProxyGenerator proxyGenerator)
      {
         _proxyGenerator = proxyGenerator;
      }

      public object GenerateProxy(Type serviceType, IInterceptor interceptor)
      {
         return _proxyGenerator.CreateInterfaceProxyWithoutTarget(serviceType, interceptor);
      }
   }
}