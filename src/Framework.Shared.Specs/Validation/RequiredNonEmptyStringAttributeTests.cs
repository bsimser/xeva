using System.Collections.Generic;
using NUnit.Framework;

namespace XEVA.Framework.Validation
{
   [TestFixture]
   public class RequiredNonEmptyStringAttributeTests
   {
      [Test]
      public void Should_add_a_message_if_the_required_property_is_a_null_or_empty_string()
      {
         Validator v = new Validator();
         RequiredNonEmptyStringAttributeSample sample = new RequiredNonEmptyStringAttributeSample();
         
         sample.StringProperty = string.Empty;
         Assert.AreEqual(1, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);

         sample.StringProperty = null;
         Assert.AreEqual(1, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);
      }
   }

   public class RequiredNonEmptyStringAttributeSample
   {
      private string _stringProperty;

      [RequiredNonEmptyString]
      public string StringProperty
      {
         get { return _stringProperty; }
         set { _stringProperty = value; }
      }

   }
}
