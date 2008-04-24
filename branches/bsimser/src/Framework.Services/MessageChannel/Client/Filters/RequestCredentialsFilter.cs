namespace XF.Services
{
   public class RequestCredentialsFilter : IChannelFilter
   {
      private ICredentialsProvider _credentialsProvider;
      private ChannelRequest _channelRequest;
      private ChannelResponse _channelResponse;

      public ICredentialsProvider CredentialsProvider
      {
         set { _credentialsProvider = value; }
      }

      public ChannelRequest ChannelRequest
      {
         get { return _channelRequest; }
         set { _channelRequest = value; }
      }

      public ChannelResponse ChannelResponse
      {
         get { return _channelResponse; }
         set { _channelResponse = value; }
      }

      public void Process()
      {
         if (_credentialsProvider.IsAuthenticating)
            _channelRequest.Message.IsAuthenticating = true;

         _channelRequest.Message.SessionTicket = _credentialsProvider.SessionTicket;
      }
   }
}