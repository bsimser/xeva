using NUnit.Framework;
using Rhino.Mocks;

namespace XEVA.Framework.Services
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
         IMessageSerializer result = MessageSerializerFactory.Create(typeof(string));
      }
   }
}