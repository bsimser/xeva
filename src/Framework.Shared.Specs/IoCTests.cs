
using Castle.Windsor;
using NUnit.Framework;

namespace XEVA.Framework.Tests
{
   [TestFixture]
   public class IoCTests
   {
      [Test]
      public void IoC_ShouldRequireInitializationWithAContainer()
      {
         Assert.IsFalse(IoC.Initialized);
         IoC.Initialize(new WindsorContainer());
         Assert.IsTrue(IoC.Initialized);
      }

   }
}
