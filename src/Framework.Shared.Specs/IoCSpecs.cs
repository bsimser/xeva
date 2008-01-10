using System;
using Castle.Windsor;
using NUnit.Framework;
using XEVA.Framework;
using XEVA.Framework.Specs;

namespace Specs_for_IoC
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
         IoC.Reset();
      }
   }

   [TestFixture]
   public class When_using_a_container : Spec
   {
      [Test]
      public void You_should_be_able_to_resolve_a_generic_type()

      {
         IoC.Initialize(new WindsorContainer());
         IoC.AddComponent("", typeof (Spec));
         Assert.AreEqual(IoC.Resolve<Spec>().GetType(), typeof (Spec));
         IoC.Reset();
      }


      [Test]
      public void You_should_be_able_to_resolve_a_generic_type_by_name()

      {
         IoC.Initialize(new WindsorContainer());
         IoC.AddComponent("test", typeof (Spec));
         Assert.AreEqual(IoC.Resolve<Spec>("test").GetType(), typeof (Spec));
         IoC.Reset();
      }
   }


   [TestFixture]
   public class When_a_container_is_reset : Spec
   {
      [Test]
      public void The_container_should_be_emptied_out()

      {
         Assert.IsFalse(IoC.Initialized);
         IoC.Initialize(new WindsorContainer());
         IoC.Reset();
         Assert.IsFalse(IoC.Initialized);
      }
   }


   [TestFixture]
   public class When_adding_a_component_to_the_container : Spec
   {
      [Test]
      public void The_new_component_should_resolved_after_being_added()
      {
         IoC.Initialize(new WindsorContainer());
         IoC.AddComponent("test", typeof (Spec));
         Assert.AreEqual(IoC.Resolve("test").GetType(), typeof (Spec));
         IoC.Reset();
      }

      [Test]
      [ExpectedException(typeof (InvalidOperationException))]
      public void An_InvalidOperationException_should_be_thrown_if_the_container_is_not_initialized()
      {
         IoC.AddComponent("test", typeof (Spec));
         IoC.Reset();
         Assert.Fail("Should have thrown exception of type InvalidOperationException");
      }
   }
}