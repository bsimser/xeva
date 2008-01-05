using Castle.Windsor;
using NUnit.Framework;
using XEVA.Framework.Specs;

namespace XEVA.Framework
{

   [TestFixture]
   public class Before_using_a_container : Spec
   {
      [Test]
      public void You_must_initialize_with_a_backing_windsor_container()
      {
         Assert.IsFalse(IoC.Initialized);
         IoC.Initialize(new WindsorContainer());
         Assert.IsTrue(IoC.Initialized);
      }
   }

}
