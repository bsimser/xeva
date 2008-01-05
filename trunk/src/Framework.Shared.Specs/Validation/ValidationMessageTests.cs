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
         ValidationMessage message1 = new ValidationMessage("Property", "Message");
         List<ValidationMessage> list = new List<ValidationMessage>();

         list.Add(message1);

         ValidationMessage message2 = new ValidationMessage("Property", "Message");

         Assert.Contains(message2, list);
      }

      [Test]
      public void NotificationMessage_ToStringOverridenForConvenience()
      {
         ValidationMessage message1 = new ValidationMessage("x", "y");
         Assert.AreEqual("Property: x, Message: y", message1.ToString());
      }
   }
}