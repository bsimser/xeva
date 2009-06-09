using System.Diagnostics;

namespace XF.Services
{
   public class DeserializeResponseFilter : IChannelFilter
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
         using (IBinaryMessageSerializer serializer = MessageSerializerFactory.CreateBinarySerializer(typeof(RequestMessage)))
         {
            _channelResponse.Message = (ResponseMessage)serializer.Deserialize(_channelResponse.Content);
         }
      }

   }
}