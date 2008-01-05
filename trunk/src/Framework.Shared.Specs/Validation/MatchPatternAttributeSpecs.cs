using System.Collections.Generic;
using NUnit.Framework;
using XEVA.Framework.Specs;
using XEVA.Framework.Validation;

// TODO: This should be refactored to a RegExAttribute
namespace Specs_for_MatchPatternAttribute
{
   [TestFixture]
   public class When_validating_an_object : Spec
   {
      private Validator _validator;
      private RequiredNonSpecialStringAttributeSample _sample;

      protected override void Before_each_spec()
      {
         _validator = new Validator();
         _sample = new RequiredNonSpecialStringAttributeSample();
      }

      [Test]
      public void Should_fail_if_the_property_does_not_match_the_regular_expression_provided()
      {
         _sample.StringProperty = "~test";
         
         IList<NotificationMessage> messages = 
            _validator.Validate(_sample, new Dictionary<string, IValidationObject>()).Messages;

         Assert.AreEqual(1, messages.Count);
      }

      [Test]
      public void Should_pass_if_the_property_does_not_match_the_regular_expression_provided()
      {
         _sample.StringProperty = "test123";

         IList<NotificationMessage> messages =
            _validator.Validate(_sample, new Dictionary<string, IValidationObject>()).Messages;
      }
   }

   public class RequiredNonSpecialStringAttributeSample
   {
      private string _stringProperty;

      [MatchPattern(@"^[0-9a-zA-Z\._-]*$")]
      public string StringProperty
      {
         get { return _stringProperty; }
         set { _stringProperty = value; }
      }

   }
}