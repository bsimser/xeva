using System;
using System.Diagnostics;
using System.Text;

namespace XF.Services
{
   public class DefaultErrorFilter : IChannelFilter
   {
      private ChannelRequest channelRequest;
      private ChannelResponse channelResponse;

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
         if(channelResponse.Message.ExceptionMessage != null)
         {
            StringBuilder errortext = new StringBuilder("Error on: " + channelResponse.Message.ExceptionMessage.ServiceKey);
            errortext.AppendLine("Exception: " + channelResponse.Message.ExceptionMessage.ExceptionType);
            errortext.AppendLine("Exception Message: " + channelResponse.Message.ExceptionMessage.ExceptionMessages[0]);
            throw new Exception(errortext.ToString());
         }
      }
   }
}