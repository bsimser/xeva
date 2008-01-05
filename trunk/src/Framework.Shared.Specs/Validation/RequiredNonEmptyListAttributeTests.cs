using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace XEVA.Framework.Validation
{
   [TestFixture]
   public class RequiredNonEmptyListAttributeTests
   {
      [Test]
      public void Should_add_a_message_if_the_required_property_is_an_empty_guid()
      {
         Validator v = new Validator();
         RequiredNonEmptyListAttributeSample sample = new RequiredNonEmptyListAttributeSample();

         Assert.AreEqual(1, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);
      }
   }

   public class RequiredNonEmptyListAttributeSample
   {
      private List<Guid> _listProperty = new List<Guid>();

      [RequiredNonEmptyList]
      public List<Guid> ListProperty
      {
         get { return _listProperty; }
         set { _listProperty = value; }
      }
   }
}