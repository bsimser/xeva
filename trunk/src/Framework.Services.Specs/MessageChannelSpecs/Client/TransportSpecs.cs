using Castle.Windsor;
using NUnit.Framework;
using Rhino.Mocks;
using XF;
using XF.Services;
using XF.Specs;

namespace Specs_for_Transport
{
   [TestFixture]
   public class When_sending_a_channle_message : Spec
   {
      private IWindsorContainer _mockContainer;
      private FakeTransport _physicalChannel;
      private Transport<FakeTransport> _theUnit;

      protected override void Before_each_spec()
      {
         _physicalChannel = Mock<FakeTransport>();
         _mockContainer = Mock<IWindsorContainer>();
         IoC.Initialize(_mockContainer);

         _theUnit = new Transport<FakeTransport>();
      }

      [Test]
      public void Instantiate_the_underlying_physical_channel()
      {
         using (Record)
         {
            Expect
               .Call(_mockContainer.Resolve<FakeTransport>())
               .Return(_physicalChannel);
         }

         using (Playback)
         {
            _theUnit.SendChannelRequest(new byte[0]);
         }
      }

      [Test] 
      public void Send_the_message_to_the_underlying_physical_channel()
      {
         SetupResult
            .For(_mockContainer.Resolve<FakeTransport>())
            .Return(_physicalChannel);

         using (Record)
         {
            Expect
               .Call(_physicalChannel.SendChannelRequest(new byte[0]))
               .Return(new byte[0]);
         }

         using (Playback)
         {
            _theUnit.SendChannelRequest(new byte[0]);
         }
      }
   }
}