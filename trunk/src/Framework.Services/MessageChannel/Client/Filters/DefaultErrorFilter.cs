using System;
using System.Diagnostics;
using System.Text;

namespace XF.Services
{
   public class DefaultErrorFilter : IChannelFilter
   {
      private ChannelRequest _channelRequest;
      private ChannelResponse _channelResponse;

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
         if(_channelResponse.Message.ExceptionMessage != null)
         {
            StringBuilder errortext = new StringBuilder("Error on: " + _channelResponse.Message.ExceptionMessage.ServiceKey);
            errortext.AppendLine("Exception: " + _channelResponse.Message.ExceptionMessage.ExceptionType);
            errortext.AppendLine("Exception Message: " + _channelResponse.Message.ExceptionMessage.ExceptionMessages[0]);
            throw new Exception(errortext.ToString());
         }
      }
   }
}