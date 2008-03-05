using System.Collections.Generic;
using NUnit.Framework;
using XF.Specs;
using XF.Validation;

namespace Specs_for_MinimumLengthAttribute
{
   [TestFixture]
   public class When_validating : Spec
   {
      private readonly Validator _validator = new Validator();

      protected override void Before_each_spec()
      {

      }

      [Test]
      public void Should_succeed_if_the_property_value_length_is_greater_than_the_length_specified()
      {
         MinimumLengthExample example = new MinimumLengthExample();
         example.SomeString = "1234567";
         Assert.AreEqual(0, _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors.Count);
      }

      [Test]
      public void Should_succeed_if_the_property_value_length_is_equal_to_the_length_specified()
      {
         MinimumLengthExample example = new MinimumLengthExample();
         example.SomeString = "123456";
         Assert.AreEqual(0, _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors.Count);
      }

      [Test]
      public void Should_fail_if_the_property_value_length_is_less_than_the_amount_specified()
      {
         MinimumLengthExample example = new MinimumLengthExample();
         example.SomeString = "12345";
         Assert.AreEqual(1, _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors.Count);
      }

      [Test]
      public void Should_ignore_properties_that_are_not_strings()
      {
         MinimumLengthExampleNonString example = new MinimumLengthExampleNonString();
         example.SomeInt = 57042;
         Assert.AreEqual(0, _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors.Count);
      }
   }

   public class MinimumLengthExample
   {
      private string _someString;

      [MinimumLength(6)]
      public string SomeString
      {
         get { return _someString; }
         set { _someString = value; }
      }
   }

   public class MinimumLengthExampleNonString
   {
      private int _someInt;

      [MinimumLength(3)]
      public int SomeInt
      {
         get { return _someInt; }
         set { _someInt = value; }
      }
   }

}
