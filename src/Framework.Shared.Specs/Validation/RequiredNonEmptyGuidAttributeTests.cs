using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace XEVA.Framework.Validation
{
   [TestFixture]
   public class RequiredNonEmptyGuidAttributeTests
   {
      [Test]
      public void Should_add_a_message_if_the_required_property_is_an_empty_guid()
      {
         Validator v = new Validator();
         RequiredNonEmptyGuidAttributeSample sample = new RequiredNonEmptyGuidAttributeSample();

         sample.GuidProperty = Guid.Empty;
         Assert.AreEqual(1, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);
      }
   }

   public class RequiredNonEmptyGuidAttributeSample
   {
      private Guid _guidProperty;

      [RequiredNonEmptyGuid]
      public Guid GuidProperty
      {
         get { return _guidProperty; }
         set { _guidProperty = value; }
      }
   }
}