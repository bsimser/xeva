using NUnit.Framework;
using XEVA.Framework.Specs;
using XEVA.Framework.Validation;

namespace Specs_for_ValidationResult
{
   [TestFixture]
   public class When_evaluating_validity : Spec
   {
      private ValidationResult _validationResult;

      protected override void Before_each_spec()
      {
         _validationResult = new ValidationResult();
      }

      [Test]
      public void Should_be_invalid_when_there_are_notification_messages()
      {
         Assert.IsTrue(_validationResult.IsValid);

         _validationResult.AddMessage(new ValidationMessage("Name", "Is required"));
         _validationResult.AddMessage("Name", "Is required");

         Assert.IsFalse(_validationResult.IsValid);
         Assert.AreEqual(1, _validationResult.Messages.Count);
      }
   }

   [TestFixture]
   public class When_requesting_a_summary_of_validation_problems : Spec
   {
      private ValidationResult _validationResult;

      protected override void Before_each_spec()
      {
         _validationResult = new ValidationResult();
      }

      [Test]
      public void Concatenate_all_messages_placing_a_new_line_between_each()
      {
         _validationResult.AddMessage(new ValidationMessage("Name", "Is required"));
         _validationResult.AddMessage(new ValidationMessage("Date", "Is required"));

         Assert.AreEqual(_validationResult.DetailedErrorMessage(),
                         "Name: Is required\r\nDate: Is required\r\n");
      }
   }
}