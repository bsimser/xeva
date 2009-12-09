using System.Web.Services.Protocols;

namespace XF.Services
{
   public class Transport<TTransport> : ITransport
      where TTransport : SoapHttpClientProtocol
   {
      private TTransport _transport;

      public virtual byte[] SendChannelRequest(byte[] requestMessage)
      {
         if (_transport == null) Initialize();

         object[] requestPrams = new object[1] { requestMessage };

         var requestResult = (byte[]) _transport.GetType().GetMethod("SendChannelRequest").Invoke(_transport, requestPrams);

         var result = GZipHelper.IonicDecompress(requestResult);

         return result;
      }

      private void Initialize()
      {
         _transport = Locator.Resolve<TTransport>();
         _transport.Timeout = 1200000;
      }
   }
}