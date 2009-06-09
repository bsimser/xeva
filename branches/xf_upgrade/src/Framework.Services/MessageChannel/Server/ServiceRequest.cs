using System.Reflection;

namespace XF.Services
{
   public class ServiceRequest
   {
      private RequestMessage _message;
      private byte[] _content;
      private object _service;
      private bool _sessionValid;
      private MethodInfo _serviceMethod;
      private object[] _serviceMethodArgs;
      private IUserAccount _userAccount;

      public ServiceRequest(byte[] content)
      {
         _content = content;
      }

      public RequestMessage Message
      {
         get { return _message; }
         set { _message = value; }
      }

      public byte[] Content
      {
         get { return _content; }
      }

      public object Service
      {
         get { return _service; }
         set { _service = value; }
      }

      public bool ValidSession
      {
         get { return _sessionValid; }
         set { _sessionValid = value; }
      }

      public MethodInfo ServiceMethod
      {
         get { return _serviceMethod; }
         set { _serviceMethod = value; }
      }

      public object[] ServiceMethodArgs
      {
         get { return _serviceMethodArgs; }
         set { _serviceMethodArgs = value; }
      }

      public IUserAccount UserAccount
      {
         get { return _userAccount; }
         set { _userAccount = value; }
      }
   }
}