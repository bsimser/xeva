namespace XF.Services
{
   public class DeserializeRequestFilter : IServiceFilter
   {
      private ServiceRequest _serviceRequest;
      private ServiceResponse _serviceResponse;

      public ServiceRequest ServiceRequest
      {
         get { return _serviceRequest; }
         set { _serviceRequest = value; }
      }

      public ServiceResponse ServiceResponse
      {
         get { return _serviceResponse; }
         set { _serviceResponse = value; }
      }

      public void Process()
      {
         using (IBinaryMessageSerializer deserializer = MessageSerializerFactory.CreateBinarySerializer(typeof(RequestMessage)))
         {
            _serviceRequest.Message = (RequestMessage)deserializer.Deserialize(_serviceRequest.Content);
         }
      }
   }
}