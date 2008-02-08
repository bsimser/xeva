namespace XF.Services
{
   public class InsertCredentialsFilter : IChannelFilter
   {
      private ICredentialsProvider _credentialsProvider;
      private RequestState _requestState;
      private ResponseState _responseState;
      private string _filterType;

      public ICredentialsProvider CredentialsProvider
      {
         get { return _credentialsProvider; }
         set { _credentialsProvider = value; }
      }

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
         _requestState.Message.SessionKey = _credentialsProvider.SessionTicket;
      }
   }
}