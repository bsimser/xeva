using System.Diagnostics;

namespace XF.Services
{
   public class DeserializeResponseFilter : IChannelFilter
   {
      private RequestState _requestState;
      private ResponseState _responseState;
      private string _filterType;

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

      public string FilterType
      {
         get { return _filterType; }
         set { _filterType = value; }
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