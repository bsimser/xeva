namespace XF.Services
{
   public class ExceptionShieldFilter : IServiceFilter
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
         if(_serviceResponse.ServiceException != null)
         {
            RequestMessage request = _serviceRequest.Message;
            string username = "unknown";
            if(_serviceRequest.UserAccount != null)
               username = _serviceRequest.UserAccount.Username;

            ExceptionShield exceptionShield = new ExceptionShield(request.ServiceKey, request.MethodKey, 
                                                                  _serviceRequest.ServiceMethodArgs, 
                                                                  _serviceResponse.ServiceException, 
                                                                  username);
            exceptionShield.Log();
            _serviceResponse.Message.ExceptionMessage = ExceptionShield.ExceptionMessage;            
         }
      }
   }
}