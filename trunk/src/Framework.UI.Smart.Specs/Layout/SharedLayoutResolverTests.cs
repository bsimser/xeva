using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.UI.Smart.Layout
{
   [TestFixture]
   public class SharedLayoutResolverTests
   {
      private MockRepository _mocks;

      [SetUp]
      public void Setup()
      {
         _mocks = new MockRepository();
      }

      [Test]
      public void Gets_a_shared_Layout_stored_in_the_Controller()
      {
         IDynamicController mockController = _mocks.CreateMock<IDynamicController>();
         ILayout stubLayout = _mocks.Stub<ILayout>();

         using (_mocks.Record())
         {
            Expect.Call(mockController.FindLayout("Blah")).Return(stubLayout);
         }

         using (_mocks.Playback())
         {
            ILayout layout = new SharedLayoutResolver(mockController, "Blah").GetLayout();
         }
      }
   }
}
