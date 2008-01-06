using System;
using NUnit.Framework;

namespace XEVA.Framework.UI.Smart
{
   [TestFixture]
   public class ComponentKeyLookupServiceTests
   {
      [Test]
      public void Standardizes_container_keys_if_necessary()
      {
         ComponentKeyLookupService theUnit = new ComponentKeyLookupService();

         string result1 = theUnit.StandardizeComponentKey(ComponentKeyType.Presenter, "SomePresenter");
         string result2 = theUnit.StandardizeComponentKey(ComponentKeyType.Layout, "SomeLayout");
         string result3 = theUnit.StandardizeComponentKey(ComponentKeyType.Command, "SomeCommand");

         Assert.AreEqual("Presenters.SomePresenter", result1);
         Assert.AreEqual("Layouts.SomeLayout", result2);
         Assert.AreEqual("Commands.SomeCommand", result3);

         string result4 =
            theUnit.StandardizeComponentKey(ComponentKeyType.Presenter, "Presenters.SomePresenter");
         string result5 = theUnit.StandardizeComponentKey(ComponentKeyType.Layout, "Layouts.SomeLayout");
         string result6 = theUnit.StandardizeComponentKey(ComponentKeyType.Command, "Commands.SomeCommand");

         Assert.AreEqual("Presenters.SomePresenter", result4);
         Assert.AreEqual("Layouts.SomeLayout", result5);
         Assert.AreEqual("Commands.SomeCommand", result6);
      }


   }
}