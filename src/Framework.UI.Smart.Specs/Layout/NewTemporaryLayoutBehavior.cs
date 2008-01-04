using System;
using System.Collections.Generic;
using System.Text;
using Castle.Windsor;
using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class NewTemporaryLayoutBehaviorTests
   {
      private MockRepository _mocks;
      

      [SetUp]
      public void Setup()
      {
         _mocks = new MockRepository();
      }

      [Test, ExpectedException(typeof(ArgumentException))]
      public void Should_not_allow_an_empty_layout_key()
      {
         NewTemporaryLayoutResolver resolver = new NewTemporaryLayoutResolver(string.Empty);
      }

      [Test, ExpectedException(typeof(ArgumentException))]
      public void Should_not_allow_a_null_layout_key()
      {
         NewTemporaryLayoutResolver resolver = new NewTemporaryLayoutResolver(null);
      }

      [Test]
      public void Should_obtain_the_layout_from_configuration()
      {
         IWindsorContainer mockContainer = _mocks.CreateMock<IWindsorContainer>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         using (_mocks.Record())
         {
            Expect.Call(mockContainer.Resolve<ILayout>("Layouts.Test")).Return(stubLayout);
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);

            NewTemporaryLayoutResolver resolver = 
               new NewTemporaryLayoutResolver("Layouts.Test");

            resolver.GetLayout();
         }
      }

      [Test]
      public void Will_only_resolve_the_layout_once_subsequent_accesses_are_cached()
      {
         IWindsorContainer mockContainer = _mocks.CreateMock<IWindsorContainer>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         using (_mocks.Record())
         {
            Expect.Call(mockContainer.Resolve<ILayout>("Layouts.Test")).Return(stubLayout);
         }

         using (_mocks.Playback())
         {
            IoC.Initialize(mockContainer);

            NewTemporaryLayoutResolver resolver =
               new NewTemporaryLayoutResolver("Layouts.Test");

            resolver.GetLayout();

            // Second attempt
            resolver.GetLayout();
         }
      }
   }
}
