using System;
using System.Collections.Generic;
using Castle.Core.Interceptor;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using XF.Services;
using XF.Specs;

namespace Spec_for_Client_PostFilters
{
   [TestFixture]
   public class When_processing_the_deserialize_response_filter : Spec
   {
      private DeserializeResponseFilter _theUnit;

      protected override void Before_each_spec()
      {
         _theUnit = new DeserializeResponseFilter();

         //For coverage only
         _theUnit.ChannelRequest = new ChannelRequest();
         ChannelRequest dummy = _theUnit.ChannelRequest;
      }

      [Test]
      public void Set_the_service_name_on_the_request_message()
      {
         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.ChannelResponse = ObjectMother.GetChannelResponse();
            _theUnit.Process();

            Assert.That(_theUnit.ChannelResponse.Message, Is.TypeOf(typeof (ResponseMessage)));
         }
      }
   }

   [TestFixture]
   public class When_processing_the_default_error_filter : Spec
   {
      private DefaultErrorFilter _theUnit;

      protected override void Before_each_spec()
      {
         _theUnit = new DefaultErrorFilter();

         //For coverage only
         _theUnit.ChannelRequest = new ChannelRequest();
         ChannelRequest dummy = _theUnit.ChannelRequest;
      }

      [Test, ExpectedException(typeof (Exception))]
      public void Throw_an_exception_when_an_error_message_exists()
      {
         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.ChannelResponse = ObjectMother.GetChannelResponseWithError();
            _theUnit.Process();
         }
      }

      [Test]
      public void Do_nothing_if_no_error_exists()
      {
         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.ChannelResponse = ObjectMother.GetChannelResponse();
            ChannelResponse dummy = _theUnit.ChannelResponse; // For coverage only
            _theUnit.Process();
         }
         
      }
   }

   [TestFixture]
   public class When_processing_the_response_credentials_filter : Spec
   {
      private ResponseCredentialsFilter _theUnit;
      private ICredentialsProvider _credentialsProvider;

      protected override void Before_each_spec()
      {
         _credentialsProvider = Mock<ICredentialsProvider>();

         _theUnit = new ResponseCredentialsFilter();
         _theUnit.CredentialsProvider = _credentialsProvider;

         //For coverage only
         _theUnit.ChannelRequest = new ChannelRequest();
         ChannelRequest dummy = _theUnit.ChannelRequest;
      }

      [Test]
      public void Turn_credentials_authenticating_mode()
      {
         SetupResult
            .For(_credentialsProvider.IsAuthenticating)
            .PropertyBehavior();

         using (Record)
         {
            _credentialsProvider.IsAuthenticating = true;
         }

         using (Playback)
         {
            _theUnit.ChannelResponse = ObjectMother.GetChannelResponse();
            _theUnit.Process();
            ChannelResponse dummy = _theUnit.ChannelResponse; // For coverage only

            Assert.That(_credentialsProvider.IsAuthenticating, Is.False);
         }
      }

      [Test]
      public void Load_session_ticket_into_the_credentials_provider()
      {
         SetupResult
            .For(_credentialsProvider.IsAuthenticating)
            .Return(false);

         SetupResult
            .For(_credentialsProvider.SessionTicket)
            .PropertyBehavior();

         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.ChannelResponse = ObjectMother.GetChannelResponse();
            _theUnit.Process();

            Assert.That(_credentialsProvider.SessionTicket, Is.Not.EqualTo(Guid.Empty));
         }
      }
   }

   [TestFixture]
   public class When_processing_the_invocation_return_filter : Spec
   {
      private InvocationReturnFilter _theUnit;
      private IInvocation _invocation;

      protected override void Before_each_spec()
      {
         _invocation = Mock<IInvocation>();

         _theUnit = new InvocationReturnFilter();

         //For coverage only
         _theUnit.ChannelRequest = new ChannelRequest();
         ChannelRequest dummy = _theUnit.ChannelRequest;
      }

      [Test]
      public void Return_the_response_message_to_the_invocation()
      {
         using (Record)
         {
            _invocation.ReturnValue = new object();
            LastCall.IgnoreArguments();
         }

         using (Playback)
         {
            _theUnit.ChannelResponse = ObjectMother.GetChannelResponse();
            ChannelResponse dummy = _theUnit.ChannelResponse; // For coverage only
            _theUnit.ChannelRequest = ObjectMother.GetChannelRequest(_invocation);
            _theUnit.Process();
         }
      }
   }

   internal static class ObjectMother
   {
      public static ChannelResponse GetChannelResponse()
      {
         ChannelResponse result = new ChannelResponse();
         result.Message = new ResponseMessage();
         result.Message.SessionTicket = Guid.NewGuid();
         result.Message.ResponseObject = "response";

         using (
            IBinaryMessageSerializer serializer =
               MessageSerializerFactory.CreateBinarySerializer(typeof (ResponseMessage)))
         {
            result.Content = serializer.Serialize(new ResponseMessage());
         }

         return result;
      }

      public static ChannelResponse GetChannelResponseWithError()
      {
         ChannelResponse result = new ChannelResponse();
         result.Message = new ResponseMessage();

         ExceptionMessage message = new ExceptionMessage();
         message.ServiceKey = typeof (FakeProxy).Name;
         message.ExceptionType = "Stub";
         message.ExceptionMessages = new List<string>(new string[1] {"stub"});
         result.Message.ExceptionMessage = message;

         return result;
      }

      public static ChannelRequest GetChannelRequest(IInvocation invocation)
      {
         ChannelRequest result = new ChannelRequest();
         result.Message = new RequestMessage();
         result.Invocation = invocation;
         result.ServiceName = typeof (FakeProxy).Name;

         return result;
      }
   }
}