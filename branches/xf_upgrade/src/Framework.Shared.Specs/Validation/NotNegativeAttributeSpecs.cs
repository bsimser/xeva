using System.Collections.Generic;
using NUnit.Framework;
using XF.Specs;
using XF.Validation;

namespace Specs_for_NotNegativeAttribute
{
   [TestFixture]
   public class When_validating_a_value : Spec
   {
      private Validator _validator;

      protected override void Before_each_spec()
      {
         _validator = new Validator();
      }

      [Test]
      public void Fail_when_the_value_is_negative()
      {
         NotEmptyExampleWithString example = new NotEmptyExampleWithString();
         example.String = "-1";

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(1, messages.Count);
      }

      [Test]
      public void Pass_when_the_value_is_not_negative()
      {
         NotEmptyExampleWithString example = new NotEmptyExampleWithString();
         example.String = "1";

         IList<ValidatonError> messages =
            _validator.Validate(new object[1] { example }, new Dictionary<string, IValidationAware>()).Errors;

         Assert.AreEqual(0, messages.Count);
      }
   }

   public class NotEmptyExampleWithString
   {
      private string _stringField;

      [NotNegative]
      public string String
      {
         get { return _stringField; }
         set { _stringField = value; }
      }
   }
}