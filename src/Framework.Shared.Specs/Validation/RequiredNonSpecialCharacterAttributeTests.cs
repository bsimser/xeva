using System.Collections.Generic;
using NUnit.Framework;
using XEVA.Framework.Validation;

namespace XEVA.Framework.Shared.Tests
{
   [TestFixture]
   public class RequiredNonSpecialCharacterAttributeTests
   {
      [Test]
      public void Should_add_a_message_if_the_required_property_is_a_null_or_empty_string()
      {
         Validator v = new Validator();
         RequiredNonSpecialStringAttributeSample sample = new RequiredNonSpecialStringAttributeSample();
         
         sample.StringProperty = string.Empty;
         Assert.AreEqual(1, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);

         sample.StringProperty = null;
         Assert.AreEqual(1, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);

         sample.StringProperty = "b@&";
         Assert.AreEqual(1, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);
      }
   }

   public class RequiredNonSpecialStringAttributeSample
   {
      private string _stringProperty;

      [RequiredNonSpecialCharacter]
      public string StringProperty
      {
         get { return _stringProperty; }
         set { _stringProperty = value; }
      }

   }
}