using System.Diagnostics;

namespace XF.Services
{
   public class DeserializeResponseFilter : IChannelFilter
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
         using (IBinaryMessageSerializer serializer = MessageSerializerFactory.CreateBinarySerializer(typeof(RequestMessage)))
         {
            channelResponse.Message = (ResponseMessage)serializer.Deserialize(channelResponse.Content);
         }
      }

   }
}