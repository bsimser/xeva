using System.Reflection;

namespace XF.Services
{
   public class InjectCredentialsFilter : IServiceFilter
   {
      private ServiceRequest _serviceRequest;
      private ServiceResponse _serviceResponse;

      public ServiceRequest ServiceRequest
      {
         get { return _serviceRequest; }
         set { _serviceRequest = value; }
      }

      public ServiceResponse ServiceResponse
      {
         get { return _serviceResponse; }
         set { _serviceResponse = value; }
      }

      public void Process()
      {
         if(_serviceRequest.UserAccount != null)
         {
            PropertyInfo userProperty = _serviceRequest.Service.GetType().GetProperty("UserAccount");
            if(userProperty != null)
            {
               userProperty.SetValue(_serviceRequest.Service, _serviceRequest.UserAccount, null);
            }
         }
      }
   }
}