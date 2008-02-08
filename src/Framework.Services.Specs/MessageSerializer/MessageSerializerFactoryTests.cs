using NUnit.Framework;
using Rhino.Mocks;

namespace XF.Services
{
   [TestFixture]
   public class MessageSerializerFactoryTests
   {
      private MockRepository _mocks;

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
      }

      [Test]
      public void Should_return_a_new_IMessageSerialzer()
      {
         IXMLMessageSerializer result = MessageSerializerFactory.CreateXMLSerializer(typeof(string));
      }
   }
}