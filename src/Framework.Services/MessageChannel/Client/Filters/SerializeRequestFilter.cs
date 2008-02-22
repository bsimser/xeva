namespace XF.Services
{
   public class SerializeRequestFilter : IChannelFilter
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
            channelRequest.Content = serializer.Serialize(channelRequest.Message);
         }
      }
   }
}