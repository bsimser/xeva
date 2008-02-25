using System;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace XF.Services
{
   public class ProxyGeneratorFactory : IProxyGeneratorFactory
   {
      private ProxyGenerator _proxyGenerator;

      public ProxyGeneratorFactory()
      {
         _proxyGenerator = new ProxyGenerator();
      }

      public object CreateInterfaceProxyWithoutTarget(Type serviceType, IInterceptor interceptor)
      {
         return _proxyGenerator.CreateInterfaceProxyWithoutTarget(serviceType, interceptor);
      }
   }
}