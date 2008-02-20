using System.Diagnostics;

namespace XF.Services
{
   public class DeserializeResponseFilter : IChannelFilter
   {
      private RequestState _requestState;
      private ResponseState _responseState;

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

      public void Process()
      {
         using (IBinaryMessageSerializer serializer = MessageSerializerFactory.CreateBinarySerializer(typeof(RequestMessage)))
         {
            _responseState.Message = (ResponseMessage)serializer.Deserialize(_responseState.Content);
         }
      }

   }
}