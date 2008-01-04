using NUnit.Framework;

namespace XEVA.Framework.Validation
{

   [TestFixture]
   public class NotificationTests
   {

      [Test]
      public void Notification_ShouldOnlyBeValidIfDoesNotContainMessages()
      {
         Notification n = new Notification();
         
         Assert.IsTrue(n.IsValid);

         n.AddMessage(new NotificationMessage("Name", "Is required"));
         n.AddMessage("Name", "Is required");

         Assert.IsFalse(n.IsValid);
         Assert.AreEqual(1, n.Messages.Count);
      }

      [Test]
      public void Should_return_a_string_concatinating_all_messages()
      {
         Notification n = new Notification();

         n.AddMessage(new NotificationMessage("Name", "Is required"));
         n.AddMessage(new NotificationMessage("Date", "Is required"));

         Assert.AreEqual(n.DetailedErrorMessage(),
            "Name: Is required\r\nDate: Is required\r\n");
      }
   }
}
