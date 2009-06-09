extern alias CastleCore;

using System.Collections.Generic;
using CastleInterception = CastleCore::Castle.Core.Interceptor;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using XF.Services;
using XF.Specs;

namespace Specs_for_ChannelIntercept
{
   [TestFixture]
   public class When_sarting : Spec
   {
      private IChannelFilter _filter;
      private CastleInterception.IInvocation _invocation;
      private ITransport _transPort;
      private ChannelIntercept _theUnit;

      protected override void Before_each_spec()
      {
         _transPort = Mock<ITransport>();
         _theUnit = new ChannelIntercept(_transPort);
         _theUnit.ServiceName = typeof (FakeProxy).Name;
         _theUnit.ServiceType = typeof(FakeProxy);
      }

      [Test]
      public void Verify_service_name_matches_types()
      {
         Assert.That(_theUnit.ServiceType.Name, Is.EqualTo(_theUnit.ServiceName));
      }
   }

   [TestFixture]
   public class When_sending_a_message : Spec
   {
      private IChannelFilter _filter;
      private CastleInterception.IInvocation _invocation;
      private ITransport _transPort;
      private ChannelIntercept _theUnit;

      protected override void Before_each_spec()
      {
         _filter = Mock<IChannelFilter>();
         _invocation = Mock<CastleInterception.IInvocation>();
         _transPort = Mock<ITransport>();
         _theUnit = new ChannelIntercept(_transPort);
         _theUnit.PreFilters = new List<IChannelFilter>();
         _theUnit.PostFilters = new List<IChannelFilter>();
      }

      [Test]
      public void Process_filters_in_the_prefilter_list()
      {
         _theUnit.PreFilters.Add(_filter);

         using (Record)
         {
            _filter.Process();
         }

         using (Playback)
         {
            _theUnit.Intercept(_invocation);
         }

      }

      [Test]
      public void Send_the_message_through_the_transport()
      {
         using (Record)
         {
            Expect
               .Call(_transPort.SendChannelRequest(null))
               .Return(new byte[0])
               .IgnoreArguments();
         }

         using (Playback)
         {
            _theUnit.Intercept(_invocation);
         }
      }

      [Test, ExpectedException(typeof(TransportFailureException))]
      public void Throw_transport_exception_on_channel_failure()
      {
         using (Record)
         {
            Expect
               .Call(_transPort.SendChannelRequest(null))
               .Throw(new TransportFailureException());
         }

         using (Playback)
         {
            _theUnit.Intercept(_invocation);
         }
      }

      [Test]
      public void Process_filters_in_the_postfilter_list()
      {
         _theUnit.PostFilters.Add(_filter);

         using (Record)
         {
            _filter.Process();
         }

         using (Playback)
         {
            _theUnit.Intercept(_invocation);
         }

      }

   }
}