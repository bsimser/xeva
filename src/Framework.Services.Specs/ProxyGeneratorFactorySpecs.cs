using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using NUnit.Framework;
using XF.Services;
using XF.Specs;

namespace Specs_for_ProxyGeneratorFactory
{
   [TestFixture]
   public class When_requesting_a_proxy : Spec
   {
      
      [Test]
      public void Return_a_dynamic_proxy_based_on_a_service_interface()
      {
         IInterceptor interceptor = Mock<IInterceptor>();
         ProxyGeneratorFactory theUnit = new ProxyGeneratorFactory(new ProxyGenerator());

         object result = theUnit.GenerateProxy(typeof (IFakeProxy), interceptor);
      }

   }
}