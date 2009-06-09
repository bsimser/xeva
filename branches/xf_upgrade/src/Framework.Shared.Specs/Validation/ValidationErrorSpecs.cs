using System.Collections.Generic;
using NUnit.Framework;
using XF.Specs;
using XF.Validation;

namespace Specs_for_ValidationError
{
   [TestFixture]
   public class When_comparing_two_validation_messages : Spec
   {
      [Test]
      public void Compare_based_on_property_and_message()
      {
         ValidatonError message1 = new ValidatonError("Property", "Message");
         List<ValidatonError> list = new List<ValidatonError>();

         list.Add(message1);

         ValidatonError message2 = new ValidatonError("Property", "Message");

         Assert.Contains(message2, list);
      }
   }


   [TestFixture]
   public class When_converting_to_a_string : Spec
   {
      [Test]
      public void Provide_a_friendly_output_for_ease_of_use()
      {
         ValidatonError message1 = new ValidatonError("x", "y");
         Assert.AreEqual("Property: x, Message: y", message1.ToString());
      }
   }
}