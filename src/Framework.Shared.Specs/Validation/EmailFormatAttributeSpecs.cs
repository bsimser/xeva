using System.Collections.Generic;
using NUnit.Framework;
using XF.Specs;
using XF.Validation;

namespace Specs_for_EmailFormatAttribute
{
   [TestFixture]
   public class When_validating : Spec
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

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(0, messages.Count);
      }

      [Test]
      public void Fail_if_the_property_is_not_a_string_value()
      {
         ExampleWithNonStringProperty example = new ExampleWithNonStringProperty();
         example.NonStringProperty = 42;

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(1, messages.Count);
      }

      [Test]
      public void Should_fail_if_the_property_value_is_null()
      {
         ExampleWithStringProperty example = new ExampleWithStringProperty();
         example.StringProperty = null;

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

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