using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.Validation
{
   [TestFixture]
   public class ValidatorTests
   {
      [Test]
      public void Validator_ShouldFindOneRequiredFieldValidationError()
      {
         Validator validator = new Validator();
         SampleValidatedClass sample1 = new SampleValidatedClass();
         Notification notification = validator.Validate(sample1, new Dictionary<string, IValidationObject>());
         Assert.IsFalse(notification.IsValid);
         Assert.AreEqual(1, notification.Messages.Count);
      }

      [Test]
      public void Should_display_error_message_on_validation_object()
      {
         MockRepository mocks = new MockRepository();
         IValidationObject validationObject = mocks.DynamicMock<IValidationObject>();

         Validator validator = new Validator();
         SampleValidatedClass sample1 = new SampleValidatedClass();
         Dictionary<string, IValidationObject> validationObjects = new Dictionary<string, IValidationObject>();
         validationObjects.Add("ReqField", validationObject);


         using (mocks.Record())
         {
            validationObject.ShowError("Some Message");
            LastCall.IgnoreArguments();
         }

         using (mocks.Playback())
         {
            validator.Validate(sample1, validationObjects);
         }
      }

      [Test]
      public void Should_clear_error_message_on_validation_object()
      {
         MockRepository mocks = new MockRepository();
         IValidationObject validationObject = mocks.DynamicMock<IValidationObject>();

         Validator validator = new Validator();
         SampleValidatedClass sample1 = new SampleValidatedClass();
         sample1.ReqField = "Some String";
         Dictionary<string, IValidationObject> validationObjects = new Dictionary<string, IValidationObject>();
         validationObjects.Add("ReqField", validationObject);


         using (mocks.Record())
         {
            validationObject.ClearError();
         }

         using (mocks.Playback())
         {
            validator.Validate(sample1, validationObjects);
         }
      }
   }

   public class SampleValidatedClass
   {
      private string _requiredField;
      private string _someEmail;

      [Required]
      public string ReqField
      {
         get { return _requiredField; }
         set { _requiredField = value; }
      }
   }
}
