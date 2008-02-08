using Castle.Core.Interceptor;

namespace XF.Services
{
   public class InvocationReturnFilter : IChannelFilter
   {
      private RequestState _requestState;
      private ResponseState _responseState;
      private string _filterType;

      public RequestState RequestState
      {
         get { return _requestState; }
         set { _requestState = value; }
      }

      public ResponseState ResponseState
      {
         get { return _responseState; }
         set { _responseState = value; }
      }

      public string FilterType
      {
         get { return _filterType; }
         set { _filterType = value; }
      }

      public void Process()
      {
         IInvocation invocation = _requestState.Invocation;
         ExceptionMessage exceptionMessage = _responseState.Message.ExceptionMessage;

         if(exceptionMessage != null)
         {
            if (exceptionMessage.ErrorLevel == "Warning" &&
                _responseState.Message.ResponseObject != null)
               invocation.ReturnValue = _responseState.Message.ResponseObject;
         }
         else
         {
            if (_responseState.Message.ResponseObject != null)
            invocation.ReturnValue = _responseState.Message.ResponseObject;
         }
      }

   }
}