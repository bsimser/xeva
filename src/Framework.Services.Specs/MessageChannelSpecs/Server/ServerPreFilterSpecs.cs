using System;
using System.ComponentModel;
using Castle.Windsor;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using XF;
using XF.Services;
using XF.Specs;

namespace Specs_for_ServerPreFilters
{
   [TestFixture]
   public class When_processing_the_deserialize_request_filter : Spec
   {
      private DeserializeRequestFilter _theUnit;

      protected override void Before_each_spec()
      {
         _theUnit = new DeserializeRequestFilter();

         //For coverage only
         _theUnit.ServiceResponse = new ServiceResponse();
         ServiceResponse dummy = _theUnit.ServiceResponse;
      }

      [Test]
      public void Build_the_request_message_from_the_channel_request()
      {
         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.ServiceRequest = ObjectMother.GetServiceRequest();
            _theUnit.Process();

            Assert.That(_theUnit.ServiceRequest.Message, Is.Not.Null);
         }
      }
   }

   [TestFixture]
   public class When_processing_the_build_service_filter : Spec
   {
      private BuildServiceFilter _theUnit;
      private IWindsorContainer _mockContainer;

      protected override void Before_each_spec()
      {
         _theUnit = new BuildServiceFilter();
         _mockContainer = Mock<IWindsorContainer>();
         Locator.Initialize(_mockContainer);

         //For coverage only
         _theUnit.ServiceResponse = new ServiceResponse();
         ServiceResponse dummy = _theUnit.ServiceResponse;
      }

      [Test]
      public void Get_the_requested_service_from_the_ioc()
      {
         using (Record)
         {
            Expect
               .Call(_mockContainer.Resolve(typeof(FakeProxy).Name))
               .Return(new FakeProxy());
         }

         using (Playback)
         {
            _theUnit.ServiceRequest = ObjectMother.GetServiceRequest();
            _theUnit.Process();

            Assert.That(_theUnit.ServiceRequest.Service, Is.TypeOf(typeof(FakeProxy)));
         }
      }
   }

   [TestFixture]
   public class When_processing_the_build_service_method_filter : Spec
   {
      private BuildServiceMethodFilter _theUnit;

      protected override void Before_each_spec()
      {
         _theUnit = new BuildServiceMethodFilter();

         //For coverage only
         _theUnit.ServiceResponse = new ServiceResponse();
         ServiceResponse dummy = _theUnit.ServiceResponse;
      }

      [Test]
      public void Get_the_requested_method_info_from_the_service()
      {
         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.ServiceRequest = ObjectMother.GetServiceRequest();
            _theUnit.ServiceRequest.Service = new FakeProxy();
            _theUnit.Process();

            Assert.That(_theUnit.ServiceRequest.ServiceMethod, Is.Not.Null);
         }
      }
   }

   [TestFixture]
   public class When_processing_the_validate_session_filter : Spec
   {
      private ValidateSessionFilter _theUnit;
      private ISessionService _sessionService;

      protected override void Before_each_spec()
      {
         _sessionService = Mock<ISessionService>();
         _theUnit = new ValidateSessionFilter();
         _theUnit.SessionService = _sessionService;

         //For coverage only
         _theUnit.ServiceResponse = new ServiceResponse();
         ServiceResponse dummy = _theUnit.ServiceResponse;
      }

      [Test]
      public void Validate_user_credentials_if_in_authenticating_mode()
      {
         ServiceRequest request = ObjectMother.GetServiceRequest();
         request.Message.IsAuthenticating = true;
         request.ServiceMethodArgs = new object[1] { new object[1] { "stub" } };

         using (Record)
         {
            Expect
               .Call(_sessionService.Authenticate(null))
               .Return(Guid.NewGuid())
               .IgnoreArguments();

            Expect
               .Call(_sessionService.GenerateSessionTicket(Guid.Empty))
               .Return(Guid.NewGuid())
               .IgnoreArguments();
         }

         using (Playback)
         {
            _theUnit.ServiceRequest = request;
            _theUnit.Process();

            Assert.That(_theUnit.ServiceRequest.ValidSession, Is.True);
         }
      }

      [Test]
      public void Retrieve_the_user_account_if_session_is_valid()
      {
         ServiceRequest request = ObjectMother.GetServiceRequest();
         request.Message.IsAuthenticating = false;
         request.Message.SessionTicket = Guid.NewGuid();
         IUserAccount account = Mock<IUserAccount>();

         using (Record)
         {
            Expect
               .Call(_sessionService.ValidateSessionTicket(Guid.NewGuid()))
               .Return(true)
               .IgnoreArguments();

            Expect
               .Call(_sessionService.GetUserAccount(Guid.Empty))
               .Return(account)
               .IgnoreArguments();
         }

         using(Playback)
         {
            _theUnit.ServiceRequest = request;
            _theUnit.Process();
         }
      }
   }

   [TestFixture]
   public class When_processing_the_inject_credentials_filter : Spec
   {
      private InjectCredentialsFilter _theUnit;

      protected override void Before_each_spec()
      {
         _theUnit = new InjectCredentialsFilter();

         //For coverage only
         _theUnit.ServiceResponse = new ServiceResponse();
         ServiceResponse dummy = _theUnit.ServiceResponse;
      }

      [Test]
      public void Load_the_user_account_in_the_requested_service_if_it_implemented()
      {
         ServiceRequest request = ObjectMother.GetServiceRequest();
         request.Message.IsAuthenticating = false;
         request.Message.SessionTicket = Guid.NewGuid();
         request.Service = new FakeProxy();
         request.UserAccount = Mock<IUserAccount>();

         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.ServiceRequest = request;
            _theUnit.Process();

            Assert.That(((FakeProxy)_theUnit.ServiceRequest.Service).UserAccount, Is.Not.Null);
         }
      }
   }

   public static class ObjectMother
   {
      public static ServiceRequest GetServiceRequest()
      {
         RequestMessage request = new RequestMessage();
         MessageArgument arg = new MessageArgument();
         arg.ArgumentType = "System.String";
         arg.Argument = "stub";
         request.MessageArgs = new MessageArgument[1]{arg};

         byte[] content;
         using (IBinaryMessageSerializer serializer =
            MessageSerializerFactory.CreateBinarySerializer(typeof(RequestMessage)))
         {
            content = serializer.Serialize(request);
         }

         ServiceRequest result = new ServiceRequest(content);
         result.Message = new RequestMessage();
         result.Message.ServiceKey = typeof (FakeProxy).Name;
         result.Message.MethodKey = "ProxyMethod";
         result.Message.MessageArgs = request.MessageArgs;
         result.Message.SessionTicket = Guid.NewGuid();

         return result;
      }

   }
}