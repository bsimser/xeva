using System;
using Castle.Windsor;
using NUnit.Framework;
using XF;
using XF.Specs;

namespace Specs_for_Locator
{
   [TestFixture]
   public class Before_resolving_a_component : Spec
   {
      [Test]
      public void You_must_initialize_with_a_backing_windsor_container()
      {
         Locator.Reset();
         Assert.IsFalse(Locator.Initialized);
         Locator.Initialize(new WindsorContainer());
         Assert.IsTrue(Locator.Initialized);
         Locator.Reset();
      }
   }

   [TestFixture]
   public class When_resolving_a_component : Spec
   {
      [Test]
      public void You_should_be_able_to_resolve_a_generic_type()
      {
         Locator.Initialize(new WindsorContainer());
         Locator.AddComponent("", typeof (Spec));
         Assert.AreEqual(Locator.Resolve<Spec>().GetType(), typeof (Spec));
         Locator.Reset();
      }

      [Test]
      public void You_should_be_able_to_resolve_a_generic_type_by_name()
      {
         Locator.Initialize(new WindsorContainer());
         Locator.AddComponent("test", typeof (Spec));
         Assert.AreEqual(Locator.Resolve<Spec>("test").GetType(), typeof (Spec));
         Locator.Reset();
      }

      [Test]
      public void You_should_be_able_to_resolve_a_component_by_key_only()
      {
         Locator.Initialize(new WindsorContainer());
         Locator.AddComponent("test", typeof(Spec));
         Assert.AreEqual(Locator.Resolve("test").GetType(), typeof(Spec));
         Locator.Reset();
      }

      [Test, ExpectedException(typeof(InvalidOperationException))]
      public void Throw_an_exception_if_the_container_is_not_initialized_when_getting_a_component_through_generics()
      {
         Locator.Resolve<Spec>();
      }

      [Test, ExpectedException(typeof(InvalidOperationException))]
      public void Throw_an_exception_if_the_container_is_not_initialized_when_getting_a_component_as_an_objects()
      {
         Locator.Resolve(string.Empty);
      }
   }


   [TestFixture]
   public class When_a_container_is_reset : Spec
   {
      [Test]
      public void The_container_should_be_emptied_out()
      {
         Locator.Reset();
         Assert.IsFalse(Locator.Initialized);
         Locator.Initialize(new WindsorContainer());
         Locator.Reset();
         Assert.IsFalse(Locator.Initialized);
      }
   }


   [TestFixture]
   public class When_adding_a_component_to_the_container : Spec
   {
      [Test]
      public void The_new_component_should_resolved_after_being_added()
      {
         Locator.Initialize(new WindsorContainer());
         Locator.AddComponent("test", typeof (Spec));
         Assert.AreEqual(Locator.Resolve("test").GetType(), typeof (Spec));
         Locator.Reset();
      }
   }

}