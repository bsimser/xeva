using System;
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
         return (byte[])_transport.GetType().GetMethod("SendChannelRequest").Invoke(_transport, requestPrams);
      }

      private void Initialize()
      {
         _transport = Locator.Resolve<TTransport>();
         _transport.Timeout = 600000;
      }
   }
}