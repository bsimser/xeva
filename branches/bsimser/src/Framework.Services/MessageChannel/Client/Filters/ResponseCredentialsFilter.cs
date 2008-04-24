using System;

namespace XF.Services
{
   public class ResponseCredentialsFilter : IChannelFilter
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
         if (_credentialsProvider.IsAuthenticating &&
             _channelResponse.Message.SessionTicket != Guid.Empty)
         {
            _credentialsProvider.IsAuthenticating = false;
         }

         _credentialsProvider.SessionTicket = _channelResponse.Message.SessionTicket;
      }
   }
}