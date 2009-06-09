using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;

namespace XF.UI.Smart
{
   [TestFixture]
   public class BindingFilterTests
   {
      private MockRepository _mocks;
      private IBindingFilter<FakeObject> _theUnit;

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _theUnit = new BindingFilter<FakeObject>();
      }

      
      [Test]
      public void Can_set_filter_string_and_initialize()
      {
         using(_mocks.Record())
         {
         
         }

         using(_mocks.Playback())
         {
            _theUnit.Initialize("A = 'A'");
         }
      }
   }
}
