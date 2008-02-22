using System;

namespace XF.Services
{
   public class ResponseCredentialsFilter : IChannelFilter
   {
      private ICredentialsProvider _credentialsProvider;
      private ChannelRequest channelRequest;
      private ChannelResponse channelResponse;

      public ICredentialsProvider CredentialsProvider
      {
         get { return _credentialsProvider; }
         set { _credentialsProvider = value; }
      }

      public ChannelRequest ChannelRequest
      {
         get { return channelRequest; }
         set { channelRequest = value; }
      }

      public ChannelResponse ChannelResponse
      {
         get { return channelResponse; }
         set { channelResponse = value; }
      }

      public void Process()
      {
         if (_credentialsProvider.IsAuthenticating &&
             channelResponse.Message.SessionTicket != Guid.Empty)
         {
            _credentialsProvider.IsAuthenticating = false;
         }

         _credentialsProvider.SessionTicket = channelResponse.Message.SessionTicket;
      }
   }
}