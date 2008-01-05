using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using XEVA.Framework.Specs;
using XEVA.Framework.Validation;

namespace Specs_for_Validator
{
   [TestFixture]
   public class When_an_object_with_validated_properties_fails_validation : Spec
   {
      [Test]
      public void Add_a_message_to_the_notification()
      {
         Validator validator = new Validator();
         ExampleClassWithValidatedProperties sample1 = new ExampleClassWithValidatedProperties();
         Notification notification = validator.Validate(sample1, new Dictionary<string, IValidationObject>());
         Assert.IsFalse(notification.IsValid);
         Assert.AreEqual(1, notification.Messages.Count);
      }

      [Test]
      public void Show_an_error_on_the_validation_controls()
      {
         IValidationObject validationObject = Mock<IValidationObject>();

         Validator validator = new Validator();
         ExampleClassWithValidatedProperties sample1 = new ExampleClassWithValidatedProperties();
         Dictionary<string, IValidationObject> validationObjects = new Dictionary<string, IValidationObject>();
         validationObjects.Add("ReqField", validationObject);

         using (Record)
         {
            validationObject.ShowError("Some Message");
            LastCall.IgnoreArguments();
         }

         using (Playback)
         {
            validator.Validate(sample1, validationObjects);
         }
      }
   }

   [TestFixture]
   public class When_an_object_with_validated_properties_passes_validation : Spec
   {
      [Test]
      public void Clear_any_errors_on_validation_controls()
      {
         IValidationObject validationObject = Mock<IValidationObject>();

         Validator validator = new Validator();
         ExampleClassWithValidatedProperties sample1 = new ExampleClassWithValidatedProperties();
         sample1.ReqField = "Some String";
         Dictionary<string, IValidationObject> validationObjects = new Dictionary<string, IValidationObject>();
         validationObjects.Add("ReqField", validationObject);

         using (Record)
         {
            validationObject.ClearError();
         }

         using (Playback)
         {
            validator.Validate(sample1, validationObjects);
         }
      }
   }

   public class ExampleClassWithValidatedProperties
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