using NUnit.Framework;
using XF.Specs;
using XF.UI.Smart;

namespace Specs_for_Request
{
   [TestFixture]
   public class When_adding_an_item_to_the_request : Spec
   {
      private Request _request;

      protected override void Before_each_spec()
      {
         _request = new Request();
      }

      [Test]
      public void The_item_should_be_retrievable_from_the_request()
      {
         object item1 = new object();

         _request.SetItem("item1", item1);

         Assert.AreEqual(item1, _request.GetItem("item1", new object()));
      }
   }

   [TestFixture]
   public class When_retrieving_an_item_from_the_request : Spec
   {
      private Request _request;

      protected override void Before_each_spec()
      {
         _request = new Request();
      }

      [Test]
      public void Return_the_empty_value_if_the_item_is_not_in_the_request()
      {
         object item1 = new object();

         Assert.AreEqual(item1, _request.GetItem("item1", item1));
      }

      [Test, ExpectedException(typeof(MissingRequiredRequestItemException))]
      public void Fail_if_a_required_item_is_not_in_the_request()
      {
         _request.GetItem("item1", new object(), true);
      }
   }
}