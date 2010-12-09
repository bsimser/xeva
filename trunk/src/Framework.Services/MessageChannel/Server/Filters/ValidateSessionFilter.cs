using System;
using System.Configuration;

namespace XF.Services {
   public class ValidateSessionFilter : IServiceFilter {
      private ServiceRequest _serviceRequest;
      private ServiceResponse _serviceResponse;
      private ISessionService _sessionService;

      public ISessionService SessionService {
         set { _sessionService = value; }
      }

      public ServiceRequest ServiceRequest {
         get { return _serviceRequest; }
         set { _serviceRequest = value; }
      }

      public ServiceResponse ServiceResponse {
         get { return _serviceResponse; }
         set { _serviceResponse = value; }
      }

      public void Process() {
         RequestMessage message = _serviceRequest.Message;

         if (message.IsAuthenticating) {
            var args = (object[])_serviceRequest.ServiceMethodArgs[0];
            Guid userID = _sessionService.Authenticate(args);
            if (userID != Guid.Empty) {
               message.SessionTicket = _sessionService.GenerateSessionTicket(userID);
               ConfigurationManager.AppSettings["SessionID"] = message.SessionTicket.ToString();
            }
            _serviceRequest.ValidSession = true;
         }
         else {
            _serviceRequest.ValidSession = ValidateSessionTicket();
            if (_serviceRequest.ValidSession) {
               LoadUserCredentials();
               ConfigurationManager.AppSettings["SessionID"] = message.SessionTicket.ToString();
            }
         }
      }

      private bool ValidateSessionTicket() {
         return _sessionService.ValidateSessionTicket(_serviceRequest.Message.SessionTicket) ||
                _serviceRequest.Message.IsAuthenticating;
      }

      private void LoadUserCredentials() {
         if (!_serviceRequest.Message.IsAuthenticating) {
            _serviceRequest.UserAccount = _sessionService.GetUserAccount(_serviceRequest.Message.SessionTicket);
         }
      }

   }
}