using System.Diagnostics;

namespace XF.Services
{
   public class DefaultErrorFilter : IChannelFilter
   {
      private RequestState _requestState;
      private ResponseState _responseState;

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

      public void Process()
      {
         if(_responseState.Message.ExceptionMessage != null)
            Debug.WriteLine(_responseState.Message.ExceptionMessage.ExceptionMessages[0]);
      }
   }
}