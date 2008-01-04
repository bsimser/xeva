using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace XEVA.Framework.UI.Smart
{

   [TestFixture]
   public class ParameterAttributeTests
   {
      [Test]
      public void Should_be_required_by_default()
      {
         ParameterAttribute parameter = new ParameterAttribute("test");
         Assert.IsTrue(parameter.IsRequired);
      }
   }
}
