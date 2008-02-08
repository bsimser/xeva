using System;
using Castle.Core;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
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
      private ITransport transport;
      private IChannelIntercept _channelIntercept;

      protected override void Before_each_spec()
      {
         _model = Mock<ComponentModel>();
         transport = Mock<ITransport>();
         _channelIntercept = Mock<IChannelIntercept>();

         _theUnit = new MessageChannel(_channelIntercept);
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
            _theUnit.InitializeChannel(null,null);
         }

      }
   }
}