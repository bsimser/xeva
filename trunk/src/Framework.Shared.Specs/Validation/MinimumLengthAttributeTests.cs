using System.Collections.Generic;
using NUnit.Framework;

namespace XEVA.Framework.Validation
{
   [TestFixture]
   public class MinimumLengthAttributeTests
   {
      [Test]
      public void MinimumLength_NonStringTypesAreIgnored()
      {
         MinimumLengthSample sample = new MinimumLengthSample();
         sample.SomeString = "abcdefghijk";

         Validator validator = new Validator();
         Assert.IsTrue(validator.Validate(sample, new Dictionary<string, IValidationObject>()).IsValid);
      }

      [Test]
      public void MinimumLength_ChecksForMinimumStringLength()
      {
         MinimumLengthSample sample = new MinimumLengthSample();
         sample.SomeString = "abcd";

         Validator validator = new Validator();
         Assert.IsTrue(validator.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count == 1);
      }

      [Test]
      public void MinimumLength_PassesWhenLengthIsExactlyTheMinimum()
      {
         MinimumLengthSample sample = new MinimumLengthSample();
         sample.SomeString = "123456";

         Validator validator = new Validator();
         Assert.IsTrue(validator.Validate(sample, new Dictionary<string, IValidationObject>()).Messages.Count == 0);
      }
   }

   public class MinimumLengthSample
   {
      private int _someInt;
      private string _someString;

      [MinimumLength(6)]
      public string SomeString
      {
         get { return _someString; }
         set { _someString = value; }
      }

      [MinimumLength(3)]
      public int SomeInt
      {
         get { return _someInt; }
         set { _someInt = value; }
      }
   }
}