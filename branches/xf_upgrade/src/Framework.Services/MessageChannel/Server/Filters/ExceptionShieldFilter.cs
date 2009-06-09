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

            object[] args;
            if (_serviceRequest.ServiceMethodArgs == null)
               args = new object[1] { "none" };
            else
               args = _serviceRequest.ServiceMethodArgs;

            ExceptionShield exceptionShield = new ExceptionShield(request.ServiceKey, request.MethodKey, 
                                                                  args, 
                                                                  _serviceResponse.ServiceException, 
                                                                  username);
            exceptionShield.Log();
            _serviceResponse.Message.ExceptionMessage = ExceptionShield.ExceptionMessage;
            _serviceResponse.Message.ResponseObject = "Error Message";
         }
      }
   }
}