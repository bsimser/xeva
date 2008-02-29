using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using XF;
using XF.Services;
using XF.Specs;

namespace Specs_for_ServerPostFilters
{
   [TestFixture]
   public class When_processing_the_exception_shield_filter : Spec
   {
      private ExceptionShieldFilter _theUnit;

      protected override void Before_each_spec()
      {
         _theUnit = new ExceptionShieldFilter();

      }

      [Test]
      public void Generate_an_exception_shield_and_load_it_into_the_response_message()
      {
         ServiceRequest request = ObjectMother.GetServiceRequest();
         request.UserAccount = Mock<IUserAccount>();
         _theUnit.ServiceRequest = request;
         ServiceRequest dummy = _theUnit.ServiceRequest; //For coverage only
         ServiceResponse response = ObjectMother.GetServiceResponse();

         using (Record)
         {
            SetupResult
               .For(request.UserAccount.Username)
               .Return("username");
         }

         using (Playback)
         {
            _theUnit.ServiceResponse = response;
            _theUnit.Process();

            Assert.That(_theUnit.ServiceResponse.Message.ExceptionMessage, Is.Not.Null);
         }
      }
   }

   [TestFixture]
   public class When_processing_the_serialize_response_filter : Spec
   {
      private SerializeResponseFilter _theUnit;

      protected override void Before_each_spec()
      {
         _theUnit = new SerializeResponseFilter();

         //For coverage only
         _theUnit.ServiceRequest = new ServiceRequest(new byte[0]);
         ServiceRequest dummy = _theUnit.ServiceRequest;
      }

      [Test]
      public void Binary_serialize_the_response_message()
      {
         using (Record) { }

         using (Playback)
         {
            _theUnit.ServiceResponse = ObjectMother.GetServiceResponse();
            _theUnit.Process();

            Assert.That(_theUnit.ServiceResponse.Content, Is.Not.Null);
         }
      }
   }

   public static class ObjectMother
   {
      internal static ServiceResponse GetServiceResponse()
      {
         ServiceResponse result = new ServiceResponse();
         result.Message = new ResponseMessage();
         result.ServiceException = new PostFilterProcessingException();

         return result;
      }

      internal static ServiceRequest GetServiceRequest()
      {
         ServiceRequest result = new ServiceRequest(new byte[0]);
         result.Message = new RequestMessage();
         result.Message.ServiceKey = typeof(FakeProxy).Name;
         result.Message.MethodKey = "ProxyMethod";
         result.ServiceMethodArgs = new object[1] { "stub" };
         return result;
      }
   }
}