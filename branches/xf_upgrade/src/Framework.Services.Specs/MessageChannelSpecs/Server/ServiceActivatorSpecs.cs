using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using XF.Services;
using XF.Specs;

namespace Specs_for_ServiceActivator
{
   [TestFixture]
   public class When_Initializing : Spec
   {
      [Test]
      public void Set_the_request_and_response_objects()
      {
         ServiceActivator theUnit = new ServiceActivator();
         theUnit.Initialize(new ServiceRequest(new byte[0]), new ServiceResponse() );
      }
   }

   [TestFixture]
   public class When_receiving_an_incoming_message : Spec
   {
      private ServiceActivator _theUnit;
      private IServiceFilter _filter;

      protected override void Before_each_spec()
      {
         _filter = Mock<IServiceFilter>();

         _theUnit = new ServiceActivator();         
      }

      [Test]
      public void Process_all_pre_filters()
      {
         List<IServiceFilter> preFilters = new List<IServiceFilter>(new IServiceFilter[]{_filter});

         using (Record)
         {
            _filter.Process();
         }

         using (Playback)
         {
            _theUnit.PreFilters = preFilters;
            _theUnit.ProcessPreFilters();
         }
      }

      [Test]
      public void Capture_prefilter_exception_on_prefilter_process_failure()
      {
         List<IServiceFilter> preFilters = new List<IServiceFilter>(new IServiceFilter[] { _filter });
         ServiceRequest request = ObjectMother.GetServiceRequest();
         ServiceResponse response = ObjectMother.GetServiceResponse();

         using (Record)
         {
            _filter.Process();
            LastCall.Throw(new PreFilterProcessingException(new Exception("stub")));
         }

         using (Playback)
         {
            _theUnit.Initialize(request, response);
            _theUnit.PreFilters = preFilters;
            _theUnit.ProcessPreFilters();

            Assert.That(response.ServiceException, Is.TypeOf(typeof(PreFilterProcessingException)));
         }
      }

      [Test]
      public void Invoke_method_call_on_service()
      {
         ServiceRequest request = ObjectMother.GetServiceRequest();
         ServiceResponse response = ObjectMother.GetServiceResponse();

         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.Initialize(request, response);
            _theUnit.InvokeServiceMethod();

            Assert.That(response.Message, Is.Not.Null);
         }
      }

      [Test]
      public void Load_session_ticket_in_the_response_message()
      {
         ServiceRequest request = ObjectMother.GetServiceRequest();
         ServiceResponse response = ObjectMother.GetServiceResponse();

         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.Initialize(request, response);
            _theUnit.InvokeServiceMethod();

            Assert.That(response.Message.SessionTicket, Is.Not.EqualTo(Guid.Empty));
         }
      }

      [Test]
      public void Capture_the_server_exception_if_method_invocation_fails()
      {
         ServiceRequest request = ObjectMother.GetServiceRequestFailure();
         ServiceResponse response = ObjectMother.GetServiceResponse();

         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.Initialize(request, response);
            _theUnit.InvokeServiceMethod();

            Assert.That(response.ServiceException, Is.TypeOf(typeof(TargetException)));
         }
      }

      [Test]
      public void Capture_an_invalid_session_exception_if_request_session_is_invalid()
      {
         ServiceRequest request = ObjectMother.GetServiceRequest();
         request.ValidSession = false;
         ServiceResponse response = ObjectMother.GetServiceResponse();

         using (Record)
         {
         }

         using (Playback)
         {
            _theUnit.Initialize(request, response);
            _theUnit.InvokeServiceMethod();

            Assert.That(response.ServiceException, Is.TypeOf(typeof(InvalidSessionException)));
         }
      }

      [Test]
      public void Process_all_post_filters()
      {
         List<IServiceFilter> postFilters = new List<IServiceFilter>(new IServiceFilter[] { _filter });

         using (Record)
         {
            _filter.Process();
         }

         using (Playback)
         {
            _theUnit.PostFilters = postFilters;
            _theUnit.ProcessPostFilters();
         }
      }

      [Test]
      public void Capture_postfilter_exception_on_postfilter_process_failure()
      {
         List<IServiceFilter> postFilters = new List<IServiceFilter>(new IServiceFilter[] { _filter });
         ServiceRequest request = ObjectMother.GetServiceRequest();
         ServiceResponse response = ObjectMother.GetServiceResponse();

         using (Record)
         {
            _filter.Process();
            LastCall.Throw(new PreFilterProcessingException(new Exception("stub")));
         }

         using (Playback)
         {
            _theUnit.Initialize(request, response);
            _theUnit.PostFilters = postFilters;
            _theUnit.ProcessPostFilters();

            Assert.That(response.ServiceException, Is.TypeOf(typeof(PostFilterProcessingException)));
         }
      }

   }

   internal static class ObjectMother
   {
      public static ServiceRequest GetServiceRequest()
      {
         ServiceRequest result = new ServiceRequest(new byte[0]);
         result.Message = new RequestMessage();
         result.Service = new FakeProxy();
         result.ServiceMethod = typeof (FakeProxy).GetMethod("ProxyMethod");
         result.ServiceMethodArgs = new object[1]{ "stub" };
         result.ValidSession = true;
         result.Message.SessionTicket = Guid.NewGuid();

         return result;
      }

      public static ServiceRequest GetServiceRequestFailure()
      {
         ServiceRequest result = new ServiceRequest(new byte[0]);
         result.Message = new RequestMessage();
         result.Service = new object();
         result.ServiceMethod = typeof(FakeProxy).GetMethod("ProxyMethod");
         result.ServiceMethodArgs = new object[1] { "stub" };
         result.ValidSession = true;
         result.Message.SessionTicket = Guid.NewGuid();

         return result;
      }

      public static ServiceResponse GetServiceResponse()
      {
         ServiceResponse result = new ServiceResponse();
         result.Message = new ResponseMessage();

         return result;
      }
   }
}