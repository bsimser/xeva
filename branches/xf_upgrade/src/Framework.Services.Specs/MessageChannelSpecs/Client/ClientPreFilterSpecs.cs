extern alias CastleCore;

using System;
using System.Reflection;
using CastleInterception = CastleCore::Castle.Core.Interceptor;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using XF.Services;
using XF.Specs;

namespace Specs_for_Client_PreFilters
{
   [TestFixture]
   public class When_processing_the_compose_request_filter : Spec
   {
      private ComposeRequestFilter _theUnit;
      private MethodInfo _proxyMethod;
      private CastleInterception.IInvocation _invocation;

      protected override void Before_each_spec()
      {
         _invocation = Mock<CastleInterception.IInvocation>();
         _proxyMethod = ObjectMother.GetMethodInfo();

         _theUnit = new ComposeRequestFilter();

         //Note just for coverage
         _theUnit.ChannelResponse = new ChannelResponse();
         ChannelResponse response = _theUnit.ChannelResponse;
      }

      [Test]
      public void Set_the_service_name_on_the_request_message()
      {
         SetupResult
            .For(_invocation.Method)
            .Return(_proxyMethod);

         SetupResult
            .For(_invocation.Arguments)
            .Return(new object[0]);

         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.ChannelRequest = ObjectMother.GetChannelRequest(_invocation);
            _theUnit.Process();
            Assert.That(_theUnit.ChannelRequest.Message.ServiceKey, Is.EqualTo(typeof(FakeProxy).Name));
         }
      }

      [Test]
      public void Set_the_method_name_on_the_request_message()
      {
         SetupResult
            .For(_invocation.Arguments)
            .Return(new object[0]);

         using (Record)
         {
            Expect
               .Call(_invocation.Method)
               .Return(ObjectMother.GetMethodInfo());
         }

         using (Playback)
         {
            _theUnit.ChannelRequest = ObjectMother.GetChannelRequest(_invocation);
            _theUnit.Process();
            Assert.That(_theUnit.ChannelRequest.Message.MethodKey, Is.EqualTo("ProxyMethod"));
         }
      }

      [Test]
      public void Set_the_method_arguments_on_the_request_message()
      {
         SetupResult
            .For(_invocation.Method)
            .Return(_proxyMethod);

         using (Record)
         {
            Expect
               .Call(_invocation.Arguments)
               .Return(ObjectMother.GetMethodArguments());
         }

         using (Playback)
         {
            _theUnit.ChannelRequest = ObjectMother.GetChannelRequest(_invocation);
            _theUnit.Process();
            Assert.That(_theUnit.ChannelRequest.Message.MessageArgs.Length, Is.EqualTo(1));
         }
      }

   }

   [TestFixture]
   public class When_processing_the_request_credentials_filter : Spec
   {
      private RequestCredentialsFilter _theUnit;
      private ICredentialsProvider _credentialsProvider;
      private CastleInterception.IInvocation _invocation;

      protected override void Before_each_spec()
      {
         _invocation = Mock<CastleInterception.IInvocation>();
         _credentialsProvider = Mock<ICredentialsProvider>();

         _theUnit = new RequestCredentialsFilter();
         _theUnit.CredentialsProvider = _credentialsProvider;

         //Note just for coverage
         _theUnit.ChannelResponse = new ChannelResponse();
         ChannelResponse response = _theUnit.ChannelResponse;
      }

      [Test]
      public void Set_the_authenticating_flag_if_credentials_provider_is_authenticating()
      {
         using (Record)
         {
            Expect
               .Call(_credentialsProvider.IsAuthenticating)
               .Return(true);
         }

         using (Playback)
         {
            _theUnit.ChannelRequest = ObjectMother.GetChannelRequest(_invocation);
            _theUnit.Process();

            Assert.That(_theUnit.ChannelRequest.Message.IsAuthenticating, Is.True);
         }
      }

      [Test]
      public void Load_the_request_session_ticket_from_the_credentials_provider()
      {
         using (Record)
         {
            Expect
               .Call(_credentialsProvider.SessionTicket)
               .Return(Guid.Empty);
         }

         using (Playback)
         {
            _theUnit.ChannelRequest = ObjectMother.GetChannelRequest(_invocation);
            _theUnit.Process();

            Assert.That(_theUnit.ChannelRequest.Message.SessionTicket, Is.EqualTo(Guid.Empty));
         }
      }

   }

   [TestFixture]
   public class When_processing_the_serialize_request_filter : Spec
   {
      private SerializeRequestFilter _theUnit;
      private CastleInterception.IInvocation _invocation;

      protected override void Before_each_spec()
      {
         _invocation = Mock<CastleInterception.IInvocation>();

         _theUnit = new SerializeRequestFilter();

         //Note just for coverage
         _theUnit.ChannelResponse = new ChannelResponse();
         ChannelResponse response = _theUnit.ChannelResponse;
      }

      [Test]
      public void Binary_serialize_the_request_message()
      {
         using (Record) {}

         using (Playback)
         {
            _theUnit.ChannelRequest = ObjectMother.GetChannelRequest(_invocation);
            _theUnit.Process();

            Assert.That(_theUnit.ChannelRequest.Content, Is.Not.Null);
         }
      }
   }

   internal static class ObjectMother
   {
      public static ChannelRequest GetChannelRequest(CastleInterception.IInvocation invocation)
      {
         ChannelRequest result = new ChannelRequest();
         result.Message = new RequestMessage();
         result.Invocation = invocation;
         result.ServiceName = typeof(FakeProxy).Name;

         return result;
      }

      public static MethodInfo GetMethodInfo()
      {
         return typeof(FakeProxy).GetMethod("ProxyMethod");
      }

      public static object[] GetMethodArguments()
      {
         object[] result = new object[1];
         result[0] = "arg1";

         return result;
      }
   }
}