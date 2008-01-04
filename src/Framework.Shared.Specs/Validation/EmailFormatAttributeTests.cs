using System.Collections.Generic;
using NUnit.Framework;

namespace XEVA.Framework.Validation
{
   [TestFixture]
   public class EmailFormatAttributeTests
   {
      [Test]
      public void EmailFormatAttribute_ProperlyValidatesAnEmailRegardlessOfDatatype()
      {
         Validator v = new Validator();
         EmailFormatAttributeTestsSample sample = new EmailFormatAttributeTestsSample();
         sample.NonStringProperty = 33;
         
         sample.StringProperty = "hello world";
         Assert.AreEqual(2, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);
         
         sample.StringProperty = null;
         Assert.AreEqual(2, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);

         sample.StringProperty = "david@laribee.com";
         Assert.AreEqual(1, v.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count);
      }
   }

   public class EmailFormatAttributeTestsSample
   {
      private int _nonStringProperty;
      private string _stringProperty;

      [EmailFormat]
      public int NonStringProperty
      {
         get { return _nonStringProperty; }
         set { _nonStringProperty = value; }
      }

      [EmailFormat]
      public string StringProperty
      {
         get { return _stringProperty; }
         set { _stringProperty = value; }
      }

   }
}
