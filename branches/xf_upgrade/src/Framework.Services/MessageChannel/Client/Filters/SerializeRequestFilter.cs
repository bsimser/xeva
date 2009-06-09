namespace XF.Services
{
   public class SerializeRequestFilter : IChannelFilter
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
            _channelRequest.Content = serializer.Serialize(_channelRequest.Message);
         }
      }
   }
}