using Castle.Core;
using NUnit.Framework;
using Rhino.Mocks;
using XF.Services;
using XF.Specs;

namespace Specs_for_MessageChannel
{
   [TestFixture]
   public class When_starting : Spec
   {
      private MessageChannel _theUnit;
      private ComponentModel _model;
      private ITransport _transport;
      private IChannelIntercept _channelIntercept;
      private IProxyGeneratorFactory _proxyFactory;

      protected override void Before_each_spec()
      {
         _model = Mock<ComponentModel>();
         _transport = Mock<ITransport>();
         _proxyFactory = Mock<IProxyGeneratorFactory>();
         _channelIntercept = Mock<IChannelIntercept>();

         _theUnit = new MessageChannel(_proxyFactory);
         _theUnit.ChannelIntercept = _channelIntercept;
      }

      [Test]
      public void Instantiate_the_channel_intercept()
      {
         using (Record)
         {
            _channelIntercept.ServiceName = null;
            LastCall.IgnoreArguments();

            _channelIntercept.ServiceType = null;
            LastCall.IgnoreArguments();
         }

         using (Playback)
         {
            _theUnit.InitializeChannel(null, null);
            IChannelIntercept interceptor = _theUnit.ChannelIntercept;
         }
      }

      [Test]
      public void Return_a_dynamic_proxy_interface_containing_the_channel_intercept()
      {
         using (Record)
         {
            Expect
               .Call(_proxyFactory.GenerateProxy(null, null))
               .Return(new object())
               .IgnoreArguments();
         }

         using (Playback)
         {
            object proxy = _theUnit.GetChannelInterface();
         }
      }
   }
}