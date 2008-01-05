using System.Collections.Generic;
using NUnit.Framework;
using XEVA.Framework.Specs;
using XEVA.Framework.Validation;

namespace Specs_for_EmailFormatAttribute
{
   [TestFixture]
   public class When_validating_a_property : Spec
   {
      private Validator _validator;

      protected override void Before_each_spec()
      {
         _validator = new Validator();
      }

      [Test]
      public void Succeed_when_the_property_is_an_email_address()
      {
         ExampleWithStringProperty example = new ExampleWithStringProperty();
         example.StringProperty = "dave@xs-go.com";

         IList<NotificationMessage> messages =
            _validator.Validate(example, new Dictionary<string, IValidationObject>()).Messages;

         Assert.AreEqual(0, messages.Count);
      }

      [Test]
      public void Fail_if_the_property_is_not_a_string_value()
      {
         ExampleWithNonStringProperty example = new ExampleWithNonStringProperty();
         example.NonStringProperty = 42;

         IList<NotificationMessage> messages =
            _validator.Validate(example, new Dictionary<string, IValidationObject>()).Messages;

         Assert.AreEqual(1, messages.Count);
      }

      [Test]
      public void Should_fail_if_the_property_value_is_null()
      {
         ExampleWithStringProperty example = new ExampleWithStringProperty();
         example.StringProperty = null;

         IList<NotificationMessage> messages =
            _validator.Validate(example, new Dictionary<string, IValidationObject>()).Messages;

         Assert.AreEqual(1, messages.Count);
      }
   }

   public class ExampleWithNonStringProperty
   {
      private int _nonStringProperty;

      [EmailFormat]
      public int NonStringProperty
      {
         get { return _nonStringProperty; }
         set { _nonStringProperty = value; }
      }
   }

   public class ExampleWithStringProperty
   {
      private string _stringProperty;

      [EmailFormat]
      public string StringProperty
      {
         get { return _stringProperty; }
         set { _stringProperty = value; }
      }
   }
}