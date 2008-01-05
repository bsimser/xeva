using NUnit.Framework;
using XEVA.Framework.Specs;
using XEVA.Framework.Validation;

namespace Specs_for_Notification
{
   [TestFixture]
   public class When_evaluating_validity : Spec
   {
      private Notification _notification;

      protected override void Before_each_spec()
      {
         _notification = new Notification();
      }

      [Test]
      public void Should_be_invalid_when_there_are_notification_messages()
      {
         Assert.IsTrue(_notification.IsValid);

         _notification.AddMessage(new NotificationMessage("Name", "Is required"));
         _notification.AddMessage("Name", "Is required");

         Assert.IsFalse(_notification.IsValid);
         Assert.AreEqual(1, _notification.Messages.Count);
      }
   }

   [TestFixture]
   public class When_requesting_a_summary_of_validation_problems : Spec
   {
      private Notification _notification;

      protected override void Before_each_spec()
      {
         _notification = new Notification();
      }

      [Test]
      public void Concatenate_all_messages_placing_a_new_line_between_each()
      {
         _notification.AddMessage(new NotificationMessage("Name", "Is required"));
         _notification.AddMessage(new NotificationMessage("Date", "Is required"));

         Assert.AreEqual(_notification.DetailedErrorMessage(),
                         "Name: Is required\r\nDate: Is required\r\n");
      }
   }
}