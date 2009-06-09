using System;
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
      public void The_full_name_of_the_type_can_be_used_as_the_key()
      {
         string data = "hello";
         _request.SetItem(data);

         Assert.AreEqual("hello", _request.GetOptionalItem<string>("System.String", null));
      }

      [Test]
      public void A_developer_can_supply_their_own_key()
      {
         string data = "hello";
         _request.SetItem("test", data);

         Assert.AreEqual("hello", _request.GetRequiredItem<string>("test", null));
      }
   }

   [TestFixture]
   public class When_adding_the_same_key_twice : Spec
   {
      [Test, ExpectedException(typeof (RequestItemAlreadySetException))]
      public void Throw_an_exception_when_there_is_an_existing_type_key()
      {
         Request request = new Request();
         request.SetItem("bob");
         request.SetItem("steve");
      }

      [Test, ExpectedException(typeof (RequestItemAlreadySetException))]
      public void Throw_an_exception_when_there_is_an_existing_developer_provided_key()
      {
         Request request = new Request();
         request.SetItem("key1", "bob");
         request.SetItem("key1", "steve");
      }

      [Test]
      public void Indicate_the_duplicated_key_in_the_exception()
      {
         try
         {
            Request request = new Request();
            request.SetItem("key1", "bob");
            request.SetItem("key1", "steve");
            Assert.Fail();
         }
         catch (RequestItemAlreadySetException ex)
         {
            Assert.AreEqual("key1", ex.Key);
         }
      }
   }

   [TestFixture]
   public class When_retrieving_an_item_of_a_different_type_but_same_key : Spec
   {
      private Request _request;
      private string _key;

      protected override void Before_each_spec()
      {
         _request = new Request();
         _key = "test";
         _request.SetItem(_key, Guid.NewGuid());
      }

      [Test, ExpectedException(typeof (RequestItemTypeMismatchException))]
      public void Throw_an_exception()
      {
         _request.GetRequiredItem<string>(_key, string.Empty);
      }

      [Test]
      public void Provide_the_key_in_the_exception()
      {
         try
         {
            _request.GetRequiredItem<string>(_key, string.Empty);
            Assert.Fail();
         }
         catch (RequestItemTypeMismatchException ex)
         {
            Assert.AreEqual(_key, ex.Key);
         }
      }

      [Test]
      public void Provide_the_type_found_in_the_exception()
      {
        try
         {
            _request.GetRequiredItem<string>(_key, string.Empty);
            Assert.Fail();
         }
         catch (RequestItemTypeMismatchException ex)
         {
            Assert.AreEqual(typeof(Guid), ex.Found);
         }
      }


      [Test]
      public void Provide_the_type_expected_in_the_exception()
      {
         try
         {
            _request.GetRequiredItem<string>(_key, string.Empty);
            Assert.Fail();
         }
         catch (RequestItemTypeMismatchException ex)
         {
            Assert.AreEqual(typeof(string), ex.Expected);
         }
      }
   }

   [TestFixture]
   public class When_retrieving_an_optional_item_from_the_request : Spec
   {
      [Test]
      public void Return_the_default_value_if_the_key_does_not_exist()
      {
         Request request = new Request();
         Assert.AreEqual("default", request.GetOptionalItem<string>("default"));
      }
   }

   [TestFixture]
   public class When_retrieving_a_required_item_from_the_request : Spec
   {
      [Test, ExpectedException(typeof (RequestItemRequiredException))]
      public void Throw_an_exception_if_the_request_item_is_not_found()
      {
         Request request = new Request();
         Assert.AreEqual("empty", request.GetRequiredItem<string>("empty"));
      }

      [Test]
      public void Provide_the_key_of_the_missing_request_item_when_an_exception_is_thrown()
      {
         try
         {
            Request request = new Request();
            Assert.AreEqual("empty", request.GetRequiredItem<string>("empty"));
            Assert.Fail();
         }
         catch (RequestItemRequiredException ex)
         {
            Assert.AreEqual("System.String", ex.Key);
         }
      }
   }
}