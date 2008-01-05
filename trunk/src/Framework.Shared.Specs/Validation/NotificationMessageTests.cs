using System.Collections.Generic;
using NUnit.Framework;

namespace XEVA.Framework.Validation
{
   [TestFixture]
   public class NotificationMessageTests
   {
      [Test]
      public void NotificationMessage_TwoMessagesAreComparableBasedOnPropertyAndMessage()
      {
         NotificationMessage message1 = new NotificationMessage("Property", "Message");
         List<NotificationMessage> list = new List<NotificationMessage>();

         list.Add(message1);

         NotificationMessage message2 = new NotificationMessage("Property", "Message");

         Assert.Contains(message2, list);
      }

      [Test]
      public void NotificationMessage_ToStringOverridenForConvenience()
      {
         NotificationMessage message1 = new NotificationMessage("x", "y");
         Assert.AreEqual("Property: x, Message: y", message1.ToString());
      }
   }
}