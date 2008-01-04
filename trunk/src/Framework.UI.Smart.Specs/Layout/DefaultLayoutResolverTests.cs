using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class DefaultLayoutResolverTests
   {
      [SetUp]
      public void Setup()
      {
         _mocks = new MockRepository();
      }

      private MockRepository _mocks;

      [Test]
      public void Uses_the_default_layout_of_the_Controller()
      {
         ILayoutLocator mockLocator = _mocks.CreateMock<ILayoutLocator>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         using (_mocks.Record())
         {
            Expect
               .Call(mockLocator.FindLayout(ControllerBuilder.DEFAULT_LAYOUT_KEY))
               .Return(stubLayout);
         }

         using (_mocks.Playback())
         {
            DefaultLayoutResolver resolver = new DefaultLayoutResolver(mockLocator);
            resolver.GetLayout();
         }
      }
   }
}